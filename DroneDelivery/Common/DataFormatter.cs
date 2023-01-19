using DroneDelivery.Domain;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DroneDelivery.Common
{
    public class DataFormatter : IDataFormatter
    {
        private readonly Regex delimiterPattern = new Regex(@"\s*,\s*");

        public DataFormatter() { }


        public (IEnumerable<Drone>, IEnumerable<Location>) ReadFile(string filePath)
        {
            var inputTemp = File.ReadAllText(filePath);

            var inputSplitTemp = inputTemp.Split("\n");

            var drones = GetDrones(inputSplitTemp[0]);

            var locationsLines = string.Join("\n",inputSplitTemp.ToList().GetRange(1, inputSplitTemp.Length - 1));

            var locations = GetLocation(locationsLines);

            return (drones, locations);
        }

        private IEnumerable<Drone> GetDrones(string input)
        {
            var drones = new List<Drone>();

            var inputSplit = delimiterPattern.Split(input);

            for (int i = 0; i < inputSplit.Length - 1; i += 2)
            {
                var drone = new Drone()
                {
                    Name = inputSplit[i].Trim(),
                    Weight = checkValidWeight(inputSplit, i, typeof(Drone))
                };

                drones.Add(drone);
            }

            var duplicatesDrone = drones.GroupBy(x => x.Name).Where(g => g.Count() > 1);

            var duplicatesName = duplicatesDrone.SelectMany(x => x.Select(y => y.Name));

            if (duplicatesName.Any())
            {
                throw new FormatException($"Duplicates drone names: {string.Join<string>(", ", duplicatesName.ToArray())}");
            }

            return drones;
        }


        private IEnumerable<Location> GetLocation(string input)
        {
            var locations = new List<Location>();
            var enumerator = input.Split('\n').GetEnumerator();
            
            while (enumerator.MoveNext())
            {
                var inputSplit = delimiterPattern.Split( (string) enumerator.Current );

                var location = new Location()
                {
                    Name = inputSplit[0].Trim(),
                    Weight = checkValidWeight(inputSplit, 0, typeof(Location))
                };

                locations.Add(location);
            }

            var duplicatesLocations = locations.GroupBy(x => x.Name).Where(g => g.Count() > 1);

            var duplicatesName = duplicatesLocations.SelectMany(x => x.Select(y => y.Name));

            if (duplicatesName.Any())
            {
                throw new FormatException($"Duplicates locations names: {string.Join<string>(", ", duplicatesName.ToArray())}");
            }

            return locations;
        }



        public string OutputTrips(IEnumerable<Trip> deliveries)
        {

            StringBuilder stringBuilder = new StringBuilder();
            var deliveriesByDrone = deliveries.GroupBy(x => x.Drone);

            var outputTrips = new List<(Drone?, IList<IEnumerable<Location>>)>();

            foreach (var delivery in deliveriesByDrone)
            {
                var xd = delivery.Select(  x=> x.Locations).ToList();
                outputTrips.Add((delivery.Key, xd));
            }


            foreach (var outputTrip in outputTrips)
            {
                stringBuilder.AppendLine(outputTrip.Item1?.ToString());                
                for (int i = 1; i <= outputTrip.Item2.Count; i++)
                {                    
                    stringBuilder.AppendLine("Trip #" + i);
                    stringBuilder.AppendLine(string.Join(", ", outputTrip.Item2[i-1]));                    
                }
                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }


        private float checkValidWeight(string[] inputSplit, int i, Type type)
        {
            float weightTemp;

            if (!float.TryParse(inputSplit[i + 1].Trim().TrimStart('[').TrimEnd(']').Trim(), out weightTemp))
            {
                throw new FormatException($"the {type.Name} '{inputSplit[i]}' weigth is not a valid number: {inputSplit[i + 1]}");
            }

            return weightTemp;
        }

    }
}
