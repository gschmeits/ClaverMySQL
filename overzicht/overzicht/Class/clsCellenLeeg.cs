using System.Security.Cryptography;
using Microsoft.Office.Tools.Excel;

namespace overzicht
{
    public static class clsCellenLeeg
    {
        public static void MaakGrootboekLeegHuis()
        {
            // Huis
            Worksheet worksheet1 = Globals.Factory.GetVstoObject(
                Globals.ThisWorkbook.Application.ActiveWorkbook.Worksheets["Huisll"]);

            clsFuncties.WriteCellByCell(62, 9, worksheet1, "0");
            clsFuncties.WriteCellByCell(63, 9, worksheet1, "0");
            clsFuncties.WriteCellByCell(65, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(66, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(67, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(68, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(69, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(70, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(71, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(72, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(74, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(75, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(76, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(77, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(78, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(80, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(81, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(82, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(83, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(84, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(86, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(87, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(88, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(89, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(90, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(93, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(94, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(95, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(96, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(97, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(99, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(100, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(101, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(102, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(107, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(108, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(109, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(110, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(111, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(112, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(113, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(116, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(117, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(118, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(119, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(120, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(121, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(124, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(125, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(126, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(129, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(130, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(131, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(133, 9, worksheet1, "0");
            clsFuncties.WriteCellByCell(135, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(136, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(137, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(138, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(140, 9, worksheet1, "0");
            clsFuncties.WriteCellByCell(142, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(143, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(144, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(146, 9, worksheet1, "0");
            clsFuncties.WriteCellByCell(31, 9, worksheet1, "0");
            clsFuncties.WriteCellByCell(32, 9, worksheet1, "0");
            clsFuncties.WriteCellByCell(33, 9, worksheet1, "0");
            clsFuncties.WriteCellByCell(34, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(35, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(37, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(38, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(40, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(41, 6, worksheet1, "");
            clsFuncties.WriteCellByCell(42, 6, worksheet1, "");
            clsFuncties.WriteCellByCell(44, 9, worksheet1, "0");
            clsFuncties.WriteCellByCell(45, 9, worksheet1, "0");
            clsFuncties.WriteCellByCell(46, 9, worksheet1, "0");
            clsFuncties.WriteCellByCell(47, 9, worksheet1, "0");
        }

        public static void MaakGrootboekLeegMissie()
        {
            // Missie
            Worksheet worksheet1 = Globals.Factory.GetVstoObject(
                Globals.ThisWorkbook.Application.ActiveWorkbook.Worksheets["Missiell"]);

            clsFuncties.WriteCellByCell(4, 10, worksheet1, "0");
            clsFuncties.WriteCellByCell(4, 4, worksheet1, "0");
            clsFuncties.WriteCellByCell(5, 4, worksheet1, "0");
            clsFuncties.WriteCellByCell(5, 10, worksheet1, "0");
            clsFuncties.WriteCellByCell(6, 10, worksheet1, "0");
            clsFuncties.WriteCellByCell(6, 4, worksheet1, "0");
            clsFuncties.WriteCellByCell(7, 10, worksheet1, "0");
            clsFuncties.WriteCellByCell(7, 4, worksheet1, "0");
            clsFuncties.WriteCellByCell(8, 10, worksheet1, "0");
            clsFuncties.WriteCellByCell(8, 4, worksheet1, "0");
            clsFuncties.WriteCellByCell(9, 10, worksheet1, "0");
            clsFuncties.WriteCellByCell(9, 4, worksheet1, "0");
            clsFuncties.WriteCellByCell(10, 10, worksheet1, "0");
            clsFuncties.WriteCellByCell(10, 4, worksheet1, "0");
            clsFuncties.WriteCellByCell(11, 10, worksheet1, "0");
            clsFuncties.WriteCellByCell(11, 4, worksheet1, "0");
            clsFuncties.WriteCellByCell(12, 10, worksheet1, "0");
            clsFuncties.WriteCellByCell(12, 4, worksheet1, "0");
            clsFuncties.WriteCellByCell(13, 10, worksheet1, "0");
            clsFuncties.WriteCellByCell(13, 4, worksheet1, "0");
            clsFuncties.WriteCellByCell(14, 10, worksheet1, "0");
            clsFuncties.WriteCellByCell(14, 4, worksheet1, "0");
            clsFuncties.WriteCellByCell(15, 4, worksheet1, "0");
            clsFuncties.WriteCellByCell(16, 4, worksheet1, "0");
            clsFuncties.WriteCellByCell(18, 4, worksheet1, "0");
        }

        public static void MaakGrootboekLeegMTL()
        {
            Worksheet worksheet1 = Globals.Factory.GetVstoObject(
                Globals.ThisWorkbook.Application.ActiveWorkbook.Worksheets["MTLl"]);

            clsFuncties.WriteCellByCell(7, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(8, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(9, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(10, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(15, 3, worksheet1, "0");
            clsFuncties.WriteCellByCell(16, 3, worksheet1, "0");
            clsFuncties.WriteCellByCell(17, 3, worksheet1, "0");
            clsFuncties.WriteCellByCell(18, 3, worksheet1, "0");
            clsFuncties.WriteCellByCell(19, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(20, 6, worksheet1, "0");
        }

        public static void MaakGrootboekLeegZaken()
        {
            Worksheet worksheet1 = Globals.Factory.GetVstoObject(
                Globals.ThisWorkbook.Application.ActiveWorkbook.Worksheets["Zakenll"]);

            clsFuncties.WriteCellByCell(7, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(8, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(9, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(10, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(11, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(16, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(17, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(18, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(19, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(20, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(34, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(35, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(36, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(37, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(38, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(39, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(40, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(41, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(42, 9, worksheet1, "0");
            clsFuncties.WriteCellByCell(43, 9, worksheet1, "0");
            clsFuncties.WriteCellByCell(44, 9, worksheet1, "0");
            clsFuncties.WriteCellByCell(45, 9, worksheet1, "0");
            clsFuncties.WriteCellByCell(59, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(60, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(61, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(62, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(63, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(66, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(67, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(69, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(70, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(71, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(72, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(73, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(74, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(75, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(76, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(78, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(79, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(81, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(82, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(83, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(84, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(85, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(86, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(87, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(88, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(90, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(91, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(92, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(93, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(96, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(97, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(98, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(99, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(100, 6, worksheet1, "0");
        }

        public static void MaakGrootboekLeegBalans()
        {
            Worksheet worksheet1 = Globals.Factory.GetVstoObject(
                Globals.ThisWorkbook.Application.ActiveWorkbook.Worksheets["Bilancio"]);
            clsFuncties.WriteCellByCell(27, 3, worksheet1, "0");
            clsFuncties.WriteCellByCell(24, 3, worksheet1, "0");
            clsFuncties.WriteCellByCell(24, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(77, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(78, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(79, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(81, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(82, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(83, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(84, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(85, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(86, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(87, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(88, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(90, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(94, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(95, 6, worksheet1, "0");
            //clsFuncties.WriteCellByCell(96, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(100, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(101, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(102, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(106, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(107, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(108, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(109, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(110, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(114, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(115, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(116, 6, worksheet1, "0");
            clsFuncties.WriteCellByCell(117, 6, worksheet1, "0");
        }

        public static void Spec336Leeg()
        {
            Worksheet worksheet1 = Globals.Factory.GetVstoObject(
                Globals.ThisWorkbook.Application.ActiveWorkbook.Worksheets["Spec336"]);
            for (var intX = 4; intX <= 54; intX++)
            {
                clsFuncties.WriteCellByCell(intX, 1, worksheet1, "");
                clsFuncties.WriteCellByCell(intX, 2, worksheet1, "");
            }
        }

        public static void Spec333Leeg()
        {
            Worksheet worksheet1 = Globals.Factory.GetVstoObject(
                Globals.ThisWorkbook.Application.ActiveWorkbook.Worksheets["Spec333"]);
            for (var intX = 4; intX <= 8; intX++)
            {
                clsFuncties.WriteCellByCell(intX, 1, worksheet1, "");
                clsFuncties.WriteCellByCell(intX, 2, worksheet1, "");
            }
        }

        public static void Spec318Leeg()
        {
            Worksheet worksheet1 = Globals.Factory.GetVstoObject(
                Globals.ThisWorkbook.Application.ActiveWorkbook.Worksheets["Spec318"]);
            for (var intX = 5; intX <= 53; intX++)
            for (var intY = 2; intY <= 5; intY++)
                clsFuncties.WriteCellByCell(intX, intY, worksheet1, "");
        }

        public static void BankgegevensLeeg()
        {
            Worksheet worksheet1 = Globals.Factory.GetVstoObject(
                Globals.ThisWorkbook.Application.ActiveWorkbook.Worksheets["Huisll"]);
            for (var intX = 52; intX <= 59; intX++) clsFuncties.WriteCellByCell(intX, 3, worksheet1, "0");
        }

        public static void LeegMetOpties(int intX1, int intX2, int intY1, int intY2, string sWorksheet, string sStr)
        {
            Worksheet worksheet1 = Globals.Factory.GetVstoObject(
                Globals.ThisWorkbook.Application.ActiveWorkbook.Worksheets[sWorksheet]);
            for (var intX = intX1; intX <= intX2; intX++)
            for (var intY = intY1; intY <= intY2; intY++)
                clsFuncties.WriteCellByCell(intX, intY, worksheet1, sStr);
        }

        public static void Spec330Leeg()
        {
            Worksheet worksheet1 = Globals.Factory.GetVstoObject(
                Globals.ThisWorkbook.Application.ActiveWorkbook.Worksheets["Spec330"]);

            clsFuncties.WriteCellByCell(5, 2, worksheet1, "");
            clsFuncties.WriteCellByCell(6, 2, worksheet1, "");
            clsFuncties.WriteCellByCell(8, 2, worksheet1, "");
            clsFuncties.WriteCellByCell(9, 2, worksheet1, "");
            clsFuncties.WriteCellByCell(10, 2, worksheet1, "");
            clsFuncties.WriteCellByCell(16, 2, worksheet1, "");
            clsFuncties.WriteCellByCell(17, 2, worksheet1, "");
        }
    }
}