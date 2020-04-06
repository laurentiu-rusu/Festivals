using System;
using System.Collections.Generic;
using Gtk;
using MusicFestivals.domains;
using MusicFestivals.repositories;
using MusicFestivals.services;
using MusicFestivals.ui;

namespace MusicFestivals
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            RepositoryConcert repositoryConcert = new RepositoryConcert();
            RepositoryTicket repositoryTicket = new RepositoryTicket();
            RepositoryUser repositoryUser = new RepositoryUser();
            
            ServiceUser serviceUser = new ServiceUser(repositoryUser);
            ServiceConcert serviceConcert = new ServiceConcert(repositoryConcert);
            ServiceTicket serviceTicket = new ServiceTicket(repositoryTicket);
            
            Application.Init();

            MainWindow mainWindow = new MainWindow("Application", serviceConcert, serviceTicket);
            LoginWindow loginWindow = new LoginWindow("Login", serviceUser);
            mainWindow.SetLoginWindow(loginWindow);
            loginWindow.SetMainWindow(mainWindow);
            
            loginWindow.ShowAll();
            Application.Run();
        }
    }
}