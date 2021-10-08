﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace Excel_generalasa_w04
{
    public partial class Form1 : Form
    {
        RealEstateEntities context = new RealEstateEntities();
        List<Flat> Flats;

        Excel.Application xlApp; // A Microsoft Excel alkalmazás
        Excel.Workbook xlWB; // A létrehozott munkafüzet
        Excel.Worksheet xlSheet; // Munkalap a munkafüzeten belül



        private string GetCell(int x, int y)//lehetővé teszi meghatározni koord alapján milyen az excel által használt koordináták 
        {
            string ExcelCoordinate = "";
            int dividend = y;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                ExcelCoordinate = Convert.ToChar(65 + modulo).ToString() + ExcelCoordinate;
                dividend = (int)((dividend - modulo) / 26);
            }
            ExcelCoordinate += x.ToString();

            return ExcelCoordinate;
        }


        public Form1()
        {
            InitializeComponent();
            LoadData();

            //datagridview1.Datasource = lakasok;//így is lehet.

            CreateExcel();
        }

        private void LoadData()
        {
             Flats = context.Flats.ToList();//memoryba másoljuk a szerverről
        }

        private void CreateExcel()
        {
            try
            {
                // Excel elindítása és az applikáció objektum betöltése
                xlApp = new Excel.Application(); //itt hozom létre az ecxelt, példányosítom

                // Új munkafüzet
                xlWB = xlApp.Workbooks.Add(Missing.Value); //füzet kigenerálásaz(üres)

                // Új munkalap
                xlSheet = xlWB.ActiveSheet; //aktuális munkalap, első oldal

                // Tábla létrehozása
                CreateTable(); // Ennek megírása a következő feladatrészben következik

                // Control átadása a felhasználónak, eddig rejtett volt az app
                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex) // Hibakezelés a beépített hibaüzenettel
            {
                //melyik soron volt a hiba
                string errMsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(errMsg, "Error");

                // Hiba esetén az Excel applikáció bezárása automatikusan
                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }
        }

        private void CreateTable()
        {
            string[] headers = new string[] {
             "Kód",
             "Eladó",
             "Oldal",
             "Kerület",
             "Lift",
             "Szobák száma",
             "Alapterület (m2)",
             "Ár (mFt)",
             "Négyzetméter ár (Ft/m2)"};

            for (int i = 0; i < headers.Length; i++)//fejlécek végéig. (sor, oszlop) beleírjuk az első sorba a tömböt
            {
                //xlSheet.Cells[1, 1] = headers[0];
                xlSheet.Cells[1,i+1]= headers[0+i];
            }

            object[,] values = new object[Flats.Count, headers.Length]; //range. egyesével másolni az adatokat lassabb lenn

            int counter = 0;
            foreach (Flat f in Flats)
            {
                values[counter, 0] = f.Code;
                values[counter, 1] = f.Vendor;
                values[counter, 2] = f.Side;
                values[counter, 3] = f.District;
                if (f.Elevator == true) values[counter, 4] = "Van";
                else values[counter, 4] = "Nincs";
                values[counter, 5] = f.NumberOfRooms;
                values[counter, 6] = f.FloorArea;
                values[counter, 7] = f.Price;
                values[counter, 8] = "=" + GetCell(counter + 2, 8) + "*1000000/" + GetCell(counter + 2, 7);//2 sor 8 oszlop * 2 sor 7 oszlop

                //values[counter. 4] = lakas.Elevator? "van": "nincs";
                //values[counter, 8] = string.Format{ }

                counter++;      
            }

            //ide másoljuk be a tartományt
            xlSheet.get_Range(
             GetCell(2, 1),
             GetCell(1 + values.GetLength(0), values.GetLength(1))).Value2 = values;//0-ik, első dimenizójából a getlengthnek

            //getCell(100, 100)-hiányzó adatok lesznek 


            Excel.Range headerRange = xlSheet.get_Range(GetCell(1, 1), GetCell(1, headers.Length));
            headerRange.Font.Bold = true;
            headerRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            headerRange.EntireColumn.AutoFit();
            headerRange.RowHeight = 40;
            headerRange.Interior.Color = Color.LightBlue;
            headerRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);

           /* int lastRowID = xlSheet.UsedRange.Rows.Count;
            Excel.Range completeRange = */


            Excel.Range tableRange = xlSheet.get_Range(GetCell(2, 1), GetCell(1 + values.GetLength(0), values.GetLength(1)));
            tableRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);

            Excel.Range firstColumn = xlSheet.get_Range(GetCell(2, 1), GetCell(1 + values.GetLength(0), 1));
            firstColumn.Font.Bold = true;
            firstColumn.Interior.Color = Color.LightYellow;

            Excel.Range lastColumn = xlSheet.get_Range(GetCell(2, values.GetLength(1)), GetCell(1 + values.GetLength(0), values.GetLength(1))); //getlengt0 - utolsó oszlop, 1 - utolsó sor
            lastColumn.NumberFormat = "###,###,00";
            lastColumn.Interior.Color = Color.LightGreen;
        }
    }

}
