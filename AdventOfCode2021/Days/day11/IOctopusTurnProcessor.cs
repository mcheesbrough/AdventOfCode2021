using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AdventOfCode2021.Days.Day2;
using AdventOfCode2021.Days.Day9;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day11
{
    public interface IOctopusTurnProcessor
    {
        Map<OctopusPoint> Process(Map<OctopusPoint> map, int turn);
    }

    public class OctopusTurnProcessor : IOctopusTurnProcessor
    {
        public Map<OctopusPoint> Process(Map<OctopusPoint> map, int turn)
        {
            var resultMap = map.Clone();
            IncrementPoints(resultMap.Points);
            Flash(map, turn);
            ResetPoints(resultMap.Points, turn);
            return resultMap;
        }

        private void Flash(Map<OctopusPoint> map, int turn)
        {
            var pointsToFlash = map.Points.Where(p => p.LastFlashTurn < turn
                                                      && p.Brightness > 9).ToList();
            if (!pointsToFlash.Any()) return;
            foreach (var point in pointsToFlash)
            {
                point.NumberOfFlashes++;
                point.LastFlashTurn = turn;
            }

            foreach (var point in pointsToFlash)
            {
                var adjacentPoints = map.AdjacentIncludingDiagonalPoints(point)
                    .Where(p => p.Brightness <= 9).ToList();
                IncrementPoints(adjacentPoints);
            }
            
            Flash(map, turn);
        }

        private void IncrementPoints(List<OctopusPoint> points)
        {
            foreach (var octopusPoint in points)
            {
                octopusPoint.Brightness++;
            }
        }

        private void ResetPoints(List<OctopusPoint> points, int turn)
        {
            foreach (var octopusPoint in points)
            {
                if (octopusPoint.LastFlashTurn == turn) octopusPoint.Brightness = 0;
            }
        }
    }
}