using System;
using System.Windows;

namespace UI_MatrixCalculator.EXMPL.Objects {
    public class Matrix {
        public Matrix(double[,] body) { // Конструктор принимаем двумерную матрицу даблов и заносит их в тело
            Body = body;
        }

        private double[,] Body { get; set; } // Тело
        
        public double GetElement(int x, int y) => Body[x, y]; // Метод получения элемента по координатам
        
        public int GetSize(int dimension) => Body.GetLength(dimension); // Метод получения длины одного из измерений

        private Vector<double> GetRow(int column) { // Метод получения строки вектором
            var temp = new double[Body.GetLength(1)]; // Создаётся вектор длиной равной длине строки матрицы 
            for (var i = 0; i < temp.Length; i++) temp[i] = Body[column, i]; // Туда копируются значения
            return new Vector<double>(temp); // Возвращаем вектор
        }

        private Vector<double> GetColumn(int row) { // Метод получения столбца вектором
            var temp = new double[Body.GetLength(0)]; // Создаётся вектор длиной равной длине строки столбца 
            for (var i = 0; i < temp.Length; i++) temp[i] = Body[i, row]; // Туда копируются значения
            return new Vector<double>(temp);  // Возвращаем вектор
        }
        
        public void SetRow(Vector<double> row, int column) { // Метод установки строки вектором
            if (row.Size() > Body.GetLength(0)) {  // Если полученный вектор больше чем есть в матрице - выводит ошибку
                MessageBox.Show("Error!");
                return;
            }

            for (var i = 0; i < row.Size(); i++) Body[column, i] = row[i]; 
        }
        
        public void SetColumn(Vector<double> column, int row) { // Метод установки столбца вектором
            if (column.Size() > Body.GetLength(1)) {  // Если полученный вектор больше чем есть в матрице - выводит ошибку
                MessageBox.Show("Error!");
                return;
            }

            for (var i = 0; i < column.Size(); i++) Body[i, row] = column[i];
        }
        
        public string Print() { // Метод вывода всей матрицы строкой
            var temp = "";
            for (var i = 0; i < Body.GetLength(0); i++) {
                for (var j = 0; j < Body.GetLength(1); j++) {
                    temp += Body[i, j] < 0 ? "" : " ";
                    temp += Body[i, j] + " ";
                }

                temp += "\n";
            }
            return temp;
        }

        public Matrix Pow() {
            var endMatrix = new Matrix(Body);
            for (var i = 0; i < GetRow(0).Size(); i++) {
                for (var j = 0; j < GetColumn(0).Size(); j++) {
                    endMatrix.Body[i, j] = Body[i, j] * Body[i, j];
                }
            }
            
            return endMatrix;
        }
        
        public static Matrix operator +(Matrix firstMatrix, Matrix secondMatrix) {
            if (firstMatrix.GetRow(0).Size() != secondMatrix.GetRow(0).Size()) return null;
            if (firstMatrix.GetColumn(0).Size() != secondMatrix.GetColumn(0).Size()) return null;

            var endMatrix = new Matrix(firstMatrix.Body);
            for (var i = 0; i < firstMatrix.GetRow(0).Size(); i++) {
                for (var j = 0; j < firstMatrix.GetColumn(0).Size(); j++) {
                    endMatrix.Body[i, j] = firstMatrix.Body[i, j] + secondMatrix.Body[i, j];
                }
            }
            
            return endMatrix;
        }
        
        public static Matrix operator +(Matrix firstMatrix, int number) {
            var endMatrix = new Matrix(firstMatrix.Body);
            for (var i = 0; i < firstMatrix.GetRow(0).Size(); i++) {
                for (var j = 0; j < firstMatrix.GetColumn(0).Size(); j++) {
                    endMatrix.Body[i, j] = firstMatrix.Body[i, j] + number;
                }
            }
            
            return endMatrix;
        }
        
        public static Matrix operator -(Matrix firstMatrix, Matrix secondMatrix) {
            if (firstMatrix.GetRow(0).Size() != secondMatrix.GetRow(0).Size()) return null;
            if (firstMatrix.GetColumn(0).Size() != secondMatrix.GetColumn(0).Size()) return null;

            var endMatrix = new Matrix(firstMatrix.Body);
            for (var i = 0; i < firstMatrix.GetRow(0).Size(); i++) {
                for (var j = 0; j < firstMatrix.GetColumn(0).Size(); j++) {
                    endMatrix.Body[i, j] = firstMatrix.Body[i, j] - secondMatrix.Body[i, j];
                }
            }
            
            return endMatrix;
        }
        
        public static Matrix operator -(Matrix firstMatrix, int number) {
            var endMatrix = new Matrix(firstMatrix.Body);
            for (var i = 0; i < firstMatrix.GetRow(0).Size(); i++) {
                for (var j = 0; j < firstMatrix.GetColumn(0).Size(); j++) {
                    endMatrix.Body[i, j] = firstMatrix.Body[i, j] - number;
                }
            }
            
            return endMatrix;
        }
        
        public static Matrix operator *(Matrix firstMatrix, Matrix secondMatrix) {
            if (firstMatrix.GetRow(0).Size() != secondMatrix.GetColumn(0).Size()) return null;

            var xSize = firstMatrix.GetColumn(0).Size();
            var ySize = secondMatrix.GetRow(0).Size();
            
            var endMatrix = new Matrix(new double[xSize, ySize]);

            for (var i = 0; i < xSize; i++) {
                for (var j = 0; j < ySize; j++) {
                    endMatrix.Body[i, j] = 0;
                    for (var k = 0; k < firstMatrix.GetRow(0).Size(); k++) {
                        endMatrix.Body[i, j] += firstMatrix.Body[i, k] * secondMatrix.Body[k, j];
                    }
                }
            }
                
            return endMatrix;
        }
        
        public static Matrix operator *(Matrix firstMatrix, int number) {
            var endMatrix = new Matrix(new double[firstMatrix.Body.GetLength(0),
                firstMatrix.Body.GetLength(1)]);
            
            for (var i = 0; i < firstMatrix.GetRow(0).Size(); i++) {
                for (var j = 0; j < firstMatrix.GetColumn(0).Size(); j++) {
                    endMatrix.Body[i, j] = 0;
                    for (var k = 0; k < firstMatrix.GetColumn(0).Size(); k++) {
                        endMatrix.Body[i, j] += firstMatrix.Body[i, k] * number;
                    }
                }
            }
            
            return endMatrix;
        }
        
        public static Matrix operator /(Matrix firstMatrix, int number) {
            var endMatrix = new Matrix(new double[firstMatrix.Body.GetLength(0),
                firstMatrix.Body.GetLength(1)]);
            
            for (var i = 0; i < firstMatrix.GetRow(0).Size(); i++) {
                for (var j = 0; j < firstMatrix.GetColumn(0).Size(); j++) {
                    endMatrix.Body[i, j] = 0;
                    for (var k = 0; k < firstMatrix.GetColumn(0).Size(); k++) {
                        endMatrix.Body[i, j] = firstMatrix.Body[i, k] / number;
                    }
                }
            }
            
            return endMatrix;
        }
    }
}
