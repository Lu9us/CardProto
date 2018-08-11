using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Unified.Services.MapService
{
    public class TileMap
    {
        public TileMap()
        {
            tileMap = new int[100, 100];
        }
        public int[,] tileMap;

        public static string pack(int[,] data)
        {
            string dat = "";
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(0); j++)
                {
                    dat += data[i,j];
                }
                dat += "\n";
            }
            return dat;
        }
        public static int[,] unpack(string dat)
        {
           int [,] data = new int[100, 100];
          
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(0); j++)
                {
                    if (dat[i + j] == '\n')
                    {
                        continue;
                    }

                    data[i, j] = int.Parse( dat[i + j].ToString());
                }
               
            }
            return data;
        }
    }
}
