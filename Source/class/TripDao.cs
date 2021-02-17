using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace WeSplitApp
{
    public class TripDao
    {
        public static BindingList<Trip> GetAll()
        {
            var results = new BindingList<Trip>();
            var folder = AppDomain.CurrentDomain.BaseDirectory + "\\database\\";
            var database = $"{folder}Database_ListTrip.txt";
            var lines = File.ReadAllLines(database);
            // Số lượng chuyến đi
            int count = int.Parse(lines[0]);
            // Chỉ số dòng
            var indexline = 1;
            // Vòng lặp đọc số chuyến đi
            for (int i = 0; i < count; i++)
            {
                var trip = new Trip();
                trip.TotalExpense = 0;
                // Đọc tên chuyến đi
                var line1 = lines[indexline++];
                var tokens1 = line1;
                trip.Name = tokens1;

                // Đọc ảnh đại diện
                var line2 = lines[indexline++];
                var tokens2 = line2;
                trip.Avatar = tokens2;

                // Đọc số lượng thành viên
                var line3 = lines[indexline++];
                var tokens3 = int.Parse(line3);

                trip.ListMembers = new BindingList<Member>();
                // Vòng lặp đọc từng thành viên
                for (int i1 = 0; i1 < tokens3; i1++)
                {
                    var member = new Member();
                    // Đọc tên thành viên
                    var line4 = lines[indexline++];
                    var tokens4 = line4.Split(new string[] { " - " }, StringSplitOptions.None); ;
                    member.Name = tokens4[0];
                    member.Avatar = tokens4[1];
                    member.Phone = tokens4[2];

                    // Đọc có phải trưởng nhóm
                    var line9 = lines[indexline++];
                    var tokens9 = line9;
                    member.Leader = tokens9;

                    trip.ListMembers.Add(member);
                }
                //Đọc số địa điểm
                var line10 = lines[indexline++];
                var tokens10 = int.Parse(line10);
                trip.ListPlaces = new BindingList<Place>();
                for (int i4 = 0; i4 < tokens10; i4++)
                {
                    //Đọc 1 địa điểm : tên - các ảnh
                    var place = new Place();
                    var line11 = lines[indexline++];
                    var tokens11 = line11;
                    place.Name = tokens11;

                    place.ListImage = new BindingList<string>();
                    var line12 = lines[indexline++];  // Đọc các ảnh
                    var tokens12 = line12.Split(new string[] { " - " }, StringSplitOptions.None);
                    for (int i5 = 0; i5 < tokens12.Length; i5++)
                    {
                        place.ListImage.Add(tokens12[i5]);
                    }
                    trip.ListPlaces.Add(place);
                }
                // Đọc số lượng chi tiêu
                var line13 = lines[indexline++];
                var tokens13 = int.Parse(line13);
                trip.ListExpenses = new BindingList<Expense>();
                for (int i6 = 0; i6 < tokens13; i6++)
                {
                    // Đọc 1 chi tiêu
                    var line14 = lines[indexline++];
                    var tokens14 = line14.Split(new string[] { " - " }, StringSplitOptions.None);
                    var expense = new Expense()
                    {
                        Name = tokens14[0],
                        Money = float.Parse(tokens14[1])
                    };
                    trip.TotalExpense += expense.Money;
                    trip.ListExpenses.Add(expense);
                }
                // Đọc trạng thái chuyến đi
                var line15 = lines[indexline++];
                string tokens15 = line15;
                trip.Status = tokens15;

                // Tính số tiền mỗi thành viên phải trả
                foreach (var _member in trip.ListMembers)
                {
                    _member.TotalCost = trip.TotalExpense / (trip.ListMembers.Count);
                }

                results.Add(trip);
            }
            return results;
        }
        public static void GhiFile(BindingList<Trip> _data, string TenFile, int LuaChon)
        {
            //var Thu = $"{folder}" + TenFile + ".txt";
            var folder = AppDomain.CurrentDomain.BaseDirectory + "\\database\\";
            var database = $"{folder}Database_ListTrip.txt";

            List<string> Recycle = new List<string>();
            Trip[] dulieu = _data.ToArray();
            if (LuaChon == 1)
            {
                Recycle.Add(_data.Count.ToString());
            }
            for (int i = 0; i < _data.Count; i++)
            {
                Recycle.Add(dulieu[i].Name);

                Recycle.Add(dulieu[i].Avatar);

                Recycle.Add(dulieu[i].ListMembers.Count.ToString());


                Member[] member = dulieu[i].ListMembers.ToArray();
                for (int jj = 0; jj < member.Count(); jj++)
                {
                    Recycle.Add(member[jj].Name + " - " + member[jj].Avatar + " - " + member[jj].Phone);
                    Recycle.Add(member[jj].Leader.ToString());
                }
                Recycle.Add(dulieu[i].ListPlaces.Count.ToString());
                Place[] place = dulieu[i].ListPlaces.ToArray();
                for (int ij = 0; ij < place.Count(); ij++)
                {
                    Recycle.Add(place[ij].Name);
                    string img = "";
                    for (int jj = 0; jj < place[ij].ListImage.Count(); jj++)
                    {
                        img = img + place[ij].ListImage[jj] + " - "; // Dư 1 " - "
                    }
                    var img1 = img.Substring(0, img.Length - 3); // Cắt bỏ " - "
                    Recycle.Add(img1);
                }
                Expense[] expenses = dulieu[i].ListExpenses.ToArray();
                Recycle.Add(expenses.Count().ToString());
                for (int i1 = 0; i1 < expenses.Count(); i1++)
                {
                    Recycle.Add(expenses[i1].Name + " - " + expenses[i1].Money.ToString());
                }
                Recycle.Add(dulieu[i].Status);
            }
            File.WriteAllLines(database, Recycle);
        }
    }
}
