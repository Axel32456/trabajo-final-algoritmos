using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Reflection;
using System.Windows.Forms;
using tp1;

namespace tpfinal
{
	public class Estrategia
	{
		public String Consulta1(List<string> datos)
		{
            List<Dato> resultados = new List<Dato>();
            Stopwatch cronometro = new Stopwatch();
            cronometro.Start();
            BuscarConHeap(datos, 5, resultados);
            cronometro.Stop();
            long tiempo1 = cronometro.ElapsedMilliseconds;

            cronometro.Reset();

            cronometro.Start();
            BuscarConOtro(datos, 5, resultados);
            cronometro.Stop();
            long tiempo2 = cronometro.ElapsedMilliseconds;

            string result = "Tiempo con Heap: " + tiempo1 + " ms   |   " + "Tiempo con Ordenamiento: " + tiempo2 + " ms";
            return result;
		}

		public String Consulta2(List<string> datos)
		{
			string result = "Implementar";

            return result;
        }

		public String Consulta3(List<string> datos)
		{
			string result = "Implementar";

            return result;
		}


        public void BuscarConOtro(List<string> datos, int cantidad, List<Dato> collected)
        {
            collected.Clear();
            Dictionary<string, int> diccionario = new Dictionary<string, int>();

            foreach (string d in datos)
            {
                if (diccionario.ContainsKey(d))
                {
                    diccionario[d]++;
                }

                else
                {
                    diccionario[d] = 1;
                }
            }

            List<Dato> lista = new List<Dato>();

            foreach (KeyValuePair<string, int> par in diccionario)
            {
                Dato dato = new Dato(par.Value, par.Key);
                lista.Add(dato);
            }

            Ordenamiento(lista);

            for (int i = 0; i < cantidad && i < lista.Count; i++)
            {
                collected.Add(lista[i]);
            }
        }


        public void BuscarConHeap(List<string> datos, int cantidad, List<Dato> collected)
        {
            collected.Clear();
            Dictionary<string, int> diccionario = new Dictionary<string, int>();
            foreach (string d in datos)
            {
                if (diccionario.ContainsKey(d))
                {
                    diccionario[d]++;
                }

                else
                {
                    diccionario[d] = 1;
                }
            }

            Dato[] heap = new Dato[diccionario.Count];
            int tamaño = 0;

            foreach (KeyValuePair<string, int> par in diccionario)
            {
                Dato dato = new Dato(par.Value, par.Key);
                heap[tamaño] = dato;
                FiltrarArriba(heap, tamaño);
                tamaño++;
            }

            int contador = 0;

            while (contador < cantidad && tamaño > 0)
            {
                collected.Add(heap[0]);
                heap[0] = heap[tamaño - 1];
                tamaño--;
                FiltrarAbajo(heap, 0, tamaño);
                contador++;
            }
        }

        private void FiltrarArriba(Dato[] heap, int i)
        {
            int padre = (i - 1) / 2;
            while (i > 0 && heap[i].ocurrencia > heap[padre].ocurrencia)
            {
                Dato temp = heap[i];
                heap[i] = heap[padre];
                heap[padre] = temp;
                i = padre;
                padre = (i - 1) / 2;
            }
        }


        private void FiltrarAbajo(Dato[] heap, int i, int size)
        {
            while (true)
            {
                int izq = 2 * i + 1;
                int der = 2 * i + 2;
                int mayor = i;

                if (izq < size && heap[izq].ocurrencia > heap[mayor].ocurrencia)
                {
                    mayor = izq;
                }

                if (der < size && heap[der].ocurrencia > heap[mayor].ocurrencia)
                {
                    mayor = der;
                }

                if (mayor != i)
                {
                    Dato temp = heap[i];
                    heap[i] = heap[mayor];
                    heap[mayor] = temp;
                    i = mayor;
                }

                else
                {
                    break;
                }
            }
        }


        private void Ordenamiento(List<Dato> lista)
        {
            int n = lista.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (lista[i].ocurrencia < lista[j].ocurrencia)
                    {
                        Dato swap = lista[i];
                        lista[i] = lista[j];
                        lista[j] = swap;
                    }
                }
            }
        }
    }
}
