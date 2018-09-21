using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IniPlusPlus {
	class Program {
		static void Main(string[] args) {
			byte[] loadFile = new byte[File.ReadAllBytes(args[0]).Length];
			loadFile = File.ReadAllBytes(args[0]);
			byte[] stringFile;

			stringFile = Encrypt(loadFile, args[2]);
			File.WriteAllBytes(args[1], stringFile);
		}

		private static byte[] Encrypt(byte[] data, string key) {
			int STRSIZE = 256;

			int[] v11 = new int[STRSIZE];
			int[] v6 = new int[STRSIZE];
			int v7 = 0;
			int v10;
			int v12;
			int v5;
			int pos2 = 0;
			byte[] returnVar = new byte[data.Length];

			for (int pos = 0; pos < STRSIZE; pos++) {
				v11[pos] = pos;
			}

			if (key != null) {
				for (int pos = 0; pos < STRSIZE; pos++) {
					if (v7 == key.Length)
						v7 = 0;
					v6[pos] = key[v7];
					v7++;
				}
			}

			v7 = 0;

			for (int pos = 0; pos < STRSIZE; pos++) {
				v7 = (v6[pos] + v11[pos] + v7) % 256;
				v10 = v11[pos];
				v11[pos] = v11[v7];
				v11[v7] = v10;
			}

			v7 = 0;

			for (int pos = 0; pos < data.Length; pos++) {
				pos2 = (pos2 + 1) % 256;
				v7 = (v7 + v11[pos2]) % 256;
				v10 = v11[pos2];
				v11[pos2] = v11[v7];
				v11[v7] = v10;
				v12 = (v11[v7] + v11[pos2]) % 256;
				v5 = v11[v12];

				returnVar[pos] = Convert.ToByte(data[pos] ^ (v5));
			}

			return returnVar;
		}
	}
}
