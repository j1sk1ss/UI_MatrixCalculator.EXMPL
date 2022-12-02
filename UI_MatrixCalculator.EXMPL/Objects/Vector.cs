using System.Linq;

namespace UI_MatrixCalculator.EXMPL.Objects
{
    public class Vector<T> { // Генерик обьект вектора прям как в ++
        public Vector(T[] body) { // Коструктор принимает массив данных и закидывает из в тело
            Body = body;
        }
        private T[] Body { get; set; } // тело
        public int Size() => Body.Length; // Метод получения длины вектора (длины тела)
        public T this[int key] {  // Метод работы [] (гетер\сеттер) с телом 
            get => Body[key];
            set => Body[key] = value;
        }
        public string PrintLikeColumn() => Body.Aggregate("", (current, t) => current + (t + "\n")); // вывод тела в столбец через LINQ
        public string PrintLikeRow() => Body.Aggregate("", (current, t) => current + (t + " ")); // вывод тела в строку через LINQ
    }
}