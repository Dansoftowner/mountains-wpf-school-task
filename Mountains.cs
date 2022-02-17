using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Györffy_Dániel_Hegyek_WPF_11A
{
    class Mountains
    {

        private List<Mountain> MountainsList;

        public int MountainsCount => MountainsList.Count;
        public double AverageHeight => MountainsList.Select(it => it.Height).Average();
        public Mountain HighestMountain => MountainsList.Aggregate((left, right) => left.Height > right.Height ? left : right);
        public List<string> DistinctMountainTerritories => MountainsList.Select(it => it.MountainTeritory).Distinct().ToList();

        public Mountains(List<Mountain> mountainsList)
        {
            MountainsList = mountainsList;
        }

        public Mountains(string filePath): this(
            File.ReadAllLines(filePath)
            .Skip(1)
            .Select(it => it.Split(";"))
            .Select(it => new Mountain(it[0], it[1], int.Parse(it[2])))
            .ToList()
        ) { }

        public List<Mountain> GetByTerritory(string territory) =>
            MountainsList.Where(it => it.MountainTeritory.Equals(territory)).ToList();

        public int GetHigherCount(int height) =>
            MountainsList.Where(it => it.Height > height).Count();

    }
}
