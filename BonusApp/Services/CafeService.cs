using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using BonusApp.Models;

namespace BonusApp.Services
{
    public class CafeService
    {
        private static readonly CafeService _instance = new CafeService();
        public static CafeService Instance => _instance;

        private readonly List<Cafe> _cafes = new()
        {
            new Cafe { ID = 1, Name = "Калипсо" },
            new Cafe { ID = 2, Name = "Розмарин" },
            new Cafe { ID = 3, Name = "Винил" },
            new Cafe { ID = 4, Name = "Мечтатели" },
            new Cafe { ID = 5, Name = "Знак" },
            new Cafe { ID = 6, Name = "Комод" }
        };
        private CafeService() { }
        public List<Cafe> GetCafes()
        {
            return _cafes.ToList();
        }
        public Cafe? GetCafeById(int id)
        {
            return _cafes.FirstOrDefault(x => x.ID == id);
        }
    }
}
