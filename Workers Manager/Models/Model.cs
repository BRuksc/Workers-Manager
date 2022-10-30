using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Workers_Manager.Tools;
using Workers_Manager.Views;

namespace Workers_Manager.Models
{
    public class Model
    {
        public List<Worker> Workers { get; set; }
        private List<Task> Tasks { get; set; }
        public Worker? SelectedWorker { get; set; } = new Worker();
        public string AddingName { get; set; } = String.Empty;
        public string AddingSurname { get; set; } = String.Empty;

        public Model()
        {
            using (var dataContext = new DataContext())
            {

                foreach (var worker in dataContext.Workers.ToList())
                {
                    int id = worker.Id;
                    worker.UnfinishedTasks
                        = dataContext.Tasks.Where(x => x.WorkerId == id).Count();
                }

                Workers = dataContext.Workers.ToList();
                Tasks = dataContext.Tasks.ToList();
            }
        }

        public bool Add()
        {
            if (AddingName != String.Empty)
            {
                int id = Workers.Max(x => x.Id) + 1;
                bool returnedValue = false;
                try
                {

                    using (var dataContext = new DataContext())
                    {
                        dataContext.Workers.Add
                            (new Worker() { Id = id, Name = AddingName, 
                                Surname=AddingSurname, UnfinishedTasks = 0 });
                        dataContext.SaveChanges();
                        Workers = dataContext.Workers.ToList();
                    }

                    AddingName = String.Empty;
                    AddingSurname = String.Empty;

                    returnedValue = true;
                }

                catch (Exception ex) 
                {
                    MessageBox.Show(ex.Message);
                }

                return returnedValue;
            }

            else return false;
        }

        public void Remove()
        {
            if (SelectedWorker != null)
            {
                try
                {
                    int selectedId = SelectedWorker.Id;

                    using (var dataContext = new DataContext())
                    {
                        if (dataContext.Workers != null)
                        {
                            dataContext.Remove(dataContext.Workers.Single(x => x.Id == selectedId));
                            dataContext.SaveChanges();

                            Workers = dataContext.Workers.ToList();
                        }
                    }

                    foreach (var i in Workers)
                    {
                        if (i.Id > selectedId)
                            i.Id--;
                    }

                    foreach (var i in Tasks)
                    {
                        if (i.WorkerId == selectedId)
                            Tasks.Remove(i);
                    }

                    using (var dataContext = new DataContext())
                    {
                         dataContext.SaveChanges();
                    }

                    SelectedWorker = null;
                }

                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
