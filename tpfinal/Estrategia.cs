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

			string result = "Tiempo con Heap: "+tiempo1+ "ms  :  "+"Tiempo con otro: "+tiempo2+ "ms";
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

            foreach(string d in datos)
            {
                if(diccionario.ContainsKey(d))
                {
                    diccionario[d] += 1; 
                } 

                else
                {
                    diccionario[d] = 1;
                }
            }

            List<Dato> lista = new List<Dato>();

            foreach(KeyValuePair<string, int> par in diccionario)
            {
                Dato dato = new Dato(par.Value, par.Key);
                lista.Add(dato);
            }

            Ordenamiento(lista);

            for(int i = 0; i < cantidad && i < lista.Count; i++)
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
                    diccionario[d] += 1;
                }
                
                else
                {
                    diccionario[d] = 1;
                }
            }
            
            List<Dato> heap = new List<Dato>();

            foreach(KeyValuePair<string, int> par in diccionario)
            {
                Dato dato = new Dato(par.Value, par.Key);

                heap.Add(dato);

                FiltrarArriba(heap, heap.Count - 1);
            }

            int contador = 0;

            while(contador < cantidad && heap.Count > 0)
            {
                collected.Add(heap[0]);

                Dato ultimo = heap[heap.Count - 1];

                heap[0] = ultimo;

                heap.RemoveAt(heap.Count - 1);

                FiltrarAbajo(heap, 0);

                contador++;
            }
        }



        private void FiltrarArriba(List<Dato> heap, int i)
        {
            int p = (i - 1) / 2;

            while (i > 0 && heap[i].ocurrencia > heap[p].ocurrencia)
            {
                Dato temp = heap[i];

                heap[i] = heap[p];

                heap[p] = temp;

                i = p;

                p = (i - 1) / 2;
            }
        }


        private void FiltrarAbajo(List<Dato> heap, int i)
        {
            int mayor = i;

            int izq = 2 * i + 1;

            int der = 2 * i + 2;

            while(izq < heap.Count)
            {
                izq = 2 * i + 1;

                der = 2 * i + 2;

                mayor = i;

                if(izq < heap.Count && heap[izq].ocurrencia > heap[mayor].ocurrencia)
                {
                    mayor = izq;
                }

                if(der < heap.Count && heap[der].ocurrencia > heap[mayor].ocurrencia)
                {
                    mayor = der;
                }

                if(mayor != i)
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


        private void Ordenamiento (List<Dato> lista)
        {
            int n = lista.Count;

            for(int i = 0; i < (n - 1); i++)
            {
                for(int j = i + 1; j < n; j++)
                {
                    if(lista[i].ocurrencia < lista[j].ocurrencia)
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
