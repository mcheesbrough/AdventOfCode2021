using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Schema;
using AdventOfCode2021.Model;

namespace AdventOfCode2021.Days.Day19
{
    public interface IScannerComparer
    {
        List<Coordinate3D> FindAllBeacons(List<Scanner> scanners, int requiredMatches);

        Transform Compare(Scanner scanner1, Scanner scanner2, int requiredMatches);
    }

    public class ScannerComparer : IScannerComparer
    {
        public List<Coordinate3D> FindAllBeacons(List<Scanner> scanners, int requiredMatches)
        {
            var results = new List<BeaconMatchResult>();
            for (var i = 0; i < scanners.Count; i++)
            {
                for (var j = 0; j < scanners.Count; j++)
                {
                    if (i == j) continue;

                    var result = Compare(scanners[i], scanners[j], requiredMatches);

                    if (result != null)
                    {
                        results.Add(new BeaconMatchResult(i, j, result));
                    }
                    Console.WriteLine($"{i}, {j}, {results.Count}");
                }
            }

            var availableMappings = new List<Tuple<int, List<BeaconMatchResult>>>();
            var final = new List<Coordinate3D>(scanners[0].BeaconCoordinates);
            scanners[0].Position = new Coordinate3D(0, 0, 0);
            var from0 = results.Where(x => x.BeaconIndex1 == 0).ToList();
            availableMappings.Add(new Tuple<int, List<BeaconMatchResult>>(0, new List<BeaconMatchResult>()));

            var alreadyMapped = new List<int>();
            var alreadyDone =
                new List<Tuple<int, int>>(from0.Select(x => new Tuple<int, int>(x.BeaconIndex1, x.BeaconIndex2)));

            while (true)
            {
                var canMap = results
                    .Where(x => availableMappings
                                    .Select(y => y.Item1).Contains(x.BeaconIndex1)
                                && !alreadyMapped.Contains(x.BeaconIndex2)
                                )
                    .ToList();
                if (!canMap.Any() ||
                    canMap.All(x =>
                        alreadyDone.Any(y => x.BeaconIndex1 == y.Item2 && x.BeaconIndex2 == y.Item1))) break;

                foreach (var toMap in canMap)
                {
                    if (alreadyMapped.Contains(toMap.BeaconIndex2)) continue;
                    if (alreadyDone.Any(x => x.Item1 == toMap.BeaconIndex2 && x.Item2 == toMap.BeaconIndex1)) continue;

                    AddMapping(availableMappings, toMap);

                    var mapping = availableMappings.First(x => x.Item1 == toMap.BeaconIndex2);
                    var translation = mapping.Item2.ToList();
                    translation.Reverse();
                    
                    var toAdd = scanners[toMap.BeaconIndex2]
                        .BeaconCoordinates
                        .Select(b =>
                        {
                            var beacon = b;
                            foreach (var t in translation)
                            {
                                beacon = beacon.Rotate(t.Transform.Rotation).Add(t.Transform.Shift);
                            }

                            return beacon;
                        })
                        .ToList();
                    final.AddRange(toAdd);

                    scanners[toMap.BeaconIndex2].Position = CalculatePositionOfScanner(translation);
                    Console.WriteLine(scanners[toMap.BeaconIndex2].Position);
                    alreadyDone.Add(new Tuple<int, int>(toMap.BeaconIndex1, toMap.BeaconIndex2));
                    alreadyMapped.Add(toMap.BeaconIndex2);
                }
            }

            var distinctFinal = final.Distinct().ToList();
            var asString = string.Join("\r\n", distinctFinal.OrderBy(x => x.X)
                .Select(x => x.ToString()));
            return distinctFinal;
        }

        private Coordinate3D CalculatePositionOfScanner(List<BeaconMatchResult> transformations)
        {
            var position = transformations[0].Transform.Shift;
            for (var i = 1; i < transformations.Count; i++)
            {
                position = transformations[i].Transform.Shift.Add(position
                    .Rotate(transformations[i].Transform.Rotation));
            }
            return position;
        }

        private void AddMapping(List<Tuple<int, List<BeaconMatchResult>>> mappings, BeaconMatchResult matchResult)
        {
            var mappingToUse = mappings.FirstOrDefault(x => x.Item1 == matchResult.BeaconIndex1);
            if (mappingToUse == null) return;
            var updatedTranslations = mappingToUse.Item2.ToList();
            updatedTranslations.Add(matchResult);
            var newMapping = new Tuple<int, List<BeaconMatchResult>>(
                matchResult.BeaconIndex2,
                updatedTranslations
            );
            mappings.Add(newMapping);
        }

        public Transform Compare(Scanner scanner1, Scanner scanner2, int requiredMatches)
        {
            //var maxMatches = 0;
            //Transform transformForMaxMatches = null;
            var possibleRotations = BuildRotations();
            foreach (var rotation in possibleRotations)
            {
                //var transform = new Transformation(x2, y2, z2, s2);
                var rotatedScanner2 = scanner2.BeaconCoordinates
                    .Select(b2 => b2.Rotate(rotation))
                    .ToList();
                foreach (var c1 in scanner1.BeaconCoordinates)
                {
                    foreach (var c2 in rotatedScanner2)
                    {
                        var translate = c1.Subtract(c2);
                        var translatedScanner2 = rotatedScanner2
                            .Select(b2 => b2.Add(translate))
                            .ToList();
                        var equalBeacons = scanner1.BeaconCoordinates
                            .Where(b => translatedScanner2.Contains(b))
                            .ToList();

                        if (equalBeacons.Count >= requiredMatches)
                        {
                            //if (equalBeacons.Count > maxMatches)
                            //{
                            //    maxMatches = equalBeacons.Count;
                            //    transformForMaxMatches = new Transform(translate, transform);
                            //}
                            Console.WriteLine($"{translatedScanner2[0]}");
                            return new Transform(translate, rotation);

                        }
                    }

                    //if (transformForMaxMatches != null)
                    //    Console.WriteLine($"Matches = {maxMatches}");
                }
            }
            return null;
        }

        private List<Rotation> BuildRotations()
        {
            var c = new Coordinate3D(1, 2, 3);
            var results = new List<Rotation>();
            var rotatedCoords = new List<Coordinate3D>();
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    for (int z = 0; z < 4; z++)
                    {
                        var rotation = new Rotation(x, y, z);
                        var result = c.Rotate(rotation);
                        if (!rotatedCoords.Contains(result))
                        {
                            rotatedCoords.Add(result);
                            results.Add(new Rotation(x, y, z));
                        }
                    }
                }
            }

            return results;
        }
    }

    public class BeaconMatchResult
    {
        public BeaconMatchResult(int beaconIndex1, int beaconIndex2, Transform transform)
        {
            BeaconIndex1 = beaconIndex1;
            BeaconIndex2 = beaconIndex2;
            Transform = transform;
        }

        public int BeaconIndex1 { get; }
        public int BeaconIndex2 { get; }
        public Transform Transform { get; }
    }

    public class Transform
    {
        public Transform(Coordinate3D shift, Rotation rotation)
        {
            Shift = shift;
            Rotation = rotation;
        }

        public Coordinate3D Shift { get; }
        public Rotation Rotation { get; }
    }

    
}

