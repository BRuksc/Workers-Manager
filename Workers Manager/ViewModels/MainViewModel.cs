using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Workers_Manager.Models;
using Workers_Manager.Tools;
using Workers_Manager.Views;
using Model = Workers_Manager.Models.Model;

namespace Workers_Manager.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Model model = new Model();
        private AddingWorkerView? addingView;

        public List<Worker> Workers
        {
            get => model.Workers;
            set
            {
                model.Workers = value;
                OnPropertyChanged(nameof(Workers));
            }
        }

        public Worker SelectedWorker
        {
            get => model.SelectedWorker;
            set
            {
                model.SelectedWorker = value;
                OnPropertyChanged(nameof(SelectedWorker));
                OnPropertyChanged(nameof(SelectedWorkerIdToString));
            }
        }

        public string AddingName
        {
            get => model.AddingName;
            set
            {
                model.AddingName = value;
                OnPropertyChanged(nameof(AddingName));
            }
        }


        public string AddingSurname
        {
            get => model.AddingSurname;
            set
            {
                model.AddingSurname = value;
                OnPropertyChanged(nameof(AddingSurname));
            }
        }
        public string SelectedWorkerIdToString
        {
            get
            {
                if (SelectedWorker != null && SelectedWorker.Id > 0)
                    return SelectedWorker.Id.ToString();

                else return "Nie wybrano pracownika!";
            }
        }

        private ICommand addNewWorker = null;

        public ICommand AddNewWorker
        {
            get
            {
                if (addNewWorker == null)
                {
                    addNewWorker = new RelayCommand(
                        (object o) =>
                        {
                            addingView = new AddingWorkerView(this);
                            addingView.Show();
                        },
                        (object o) =>
                        {
                            return Workers != null;
                        });
                }

                return addNewWorker;
            }
        }


        private ICommand add = null;

        public ICommand Add
        {
            get
            {
                if (add == null)
                {
                    add = new RelayCommand(
                        (object o) =>
                        {
                            var canBeClosed = model.Add();

                            OnPropertyChanged(nameof(Workers));
                            OnPropertyChanged(nameof(AddingName));
                            OnPropertyChanged(nameof(AddingSurname));

                            if (canBeClosed)
                            {
                                addingView.Close();
                                AddingName = String.Empty;
                            }

                            else MessageBox.Show("Programm cannot add an empty worker!");
                        },
                        (object o) =>
                        {
                            return AddingName != null;
                        });
                }

                return add;
            }
        }

        private ICommand remove = null;

        public ICommand Remove
        {
            get
            {
                if (remove == null)
                {
                    remove = new RelayCommand(
                       (object o) =>
                       {
                           model.Remove();
                           OnPropertyChanged(nameof(SelectedWorker));
                           OnPropertyChanged(nameof(Workers));
                       },
                       (object o) =>
                       {
                           return SelectedWorker != null;
                       });
                }

                return remove;
            }
        }
    }
}
