using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MolConstructor
{
  public class Bond
    {
      public int Index;
      public List<int> Neighbors;

      public Bond(int index, int neighbor)
      {
          Index = index;
          Neighbors = new List<int>();
          Neighbors.Add(neighbor);
      }


      public static List<Bond> OrderBonds(List<int[]> bonds)
      {
          var listB = new List<Bond>();

          foreach (var c in bonds)
          {
              if (listB.Count == 0)
              {
                  listB.Add(new Bond(c[0],c[1]));
                  listB.Add(new Bond(c[1],c[0]));
              }
              else
              {
                  var firstInd = listB.FindIndex(x=>x.Index==c[0]);
                  var secondInd = listB.FindIndex(x=>x.Index==c[1]);

                  if (firstInd != -1)
                  {
                      listB[firstInd].Neighbors.Add(c[1]);
                  }
                  else
                  {
                      listB.Add(new Bond(c[0],c[1]));
                  }

                  if(secondInd !=-1)
                  {
                       listB[secondInd].Neighbors.Add(c[1]);
                  }
                  else
                  {
                      listB.Add(new Bond(c[1], c[0]));
                  }
              }
          }

          listB = listB.OrderBy(x => x.Index).ToList();

          foreach (var b in listB)
          {
              b.Neighbors.Sort();
          }

          return listB;
      }
    }
}
