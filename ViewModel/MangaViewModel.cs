﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MangaTracker.Services;

namespace MangaTracker.ViewModel
{
    public partial class MangaViewModel : BaseViewModel
    {
        public ObservableCollection<Manga> MangaList { get; } = new();
        public Command GetMangaCommand { get; }
        MangaService mangaService;
        public MangaViewModel()
        {
            Title = "Manga Tracker";
            this.mangaService = mangaService;
            GetMangaCommand = new Command(async () => await GetMangaAsync());
        }
        async Task GetMangaAsync()
        {
            if (IsBusy)
            {
                return;
            }
            try
            {
                IsBusy = true;
                var mangas = await mangaService.GetManga();
                if(MangaList.Count != 0)
                {
                    MangaList.Clear();
                }
                foreach(var manga in mangas)
                {
                    MangaList.Add(manga);
                }
            }
            catch(Exception e) { 
                Console.WriteLine(e.Message);
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
