using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace MuhasebeFORM
{
    class MySqlDB
    {
        MySqlConnection baglanti;
        public bool mesaj =false;
        public MySqlDataAdapter da;
        public DataTable dt;
        public List<object> musteriList = new List<object>();
        public int musteriSize;
        public string hataMesaji;

        public int cariid;
        private string aciklamaSirket;
        private int tutarSirket;

        public MySqlDB()
        {
            try
            {
                baglanti = new MySqlConnection("Server=localhost;Database=muhasebedb;Uid=root;Pwd='password';");
                baglanti.Open();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        public void girisYap(string ad, string sifre)
        {
            MySqlDataAdapter sda = new MySqlDataAdapter("select count(*) from kullanici where ad= '" + ad+ "' and sifre = '" + sifre+ "'", baglanti);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                mesaj = true;
            }
            else
            {
                mesaj = false;
            }
        }

        public void addMusteri(string tckn, string adsoyad, string telefon, string email)
        {
            try
            {


                MySqlCommand comm = baglanti.CreateCommand();
                comm.CommandText = "INSERT INTO musteri(tckn,ad, telefon,email) VALUES(@tckn, @ad,@telefon,@email)";
                comm.Parameters.AddWithValue("@tckn", tckn);
                comm.Parameters.AddWithValue("@ad", adsoyad);
                comm.Parameters.AddWithValue("@telefon", telefon);
                comm.Parameters.AddWithValue("@email", email);
                comm.ExecuteNonQuery();
                mesaj = true;
            }
            catch(Exception ex)
            {
                mesaj = false;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public void addCari(string hizmetad, string aciklama, string tarih, int tutar, int musteriid)
        {
            try
            {


                MySqlCommand comm = baglanti.CreateCommand();
                comm.CommandText = "INSERT INTO cari(hizmetad,aciklama, tarih,tutar,musteriid) VALUES(@hizmetad, @aciklama,@tarih,@tutar,@musteriid)";
                comm.Parameters.AddWithValue("@hizmetad", hizmetad);
                comm.Parameters.AddWithValue("@aciklama", aciklama);
                comm.Parameters.AddWithValue("@tarih", tarih);
                comm.Parameters.AddWithValue("@tutar", tutar);
                comm.Parameters.AddWithValue("@musteriid", musteriid);
                comm.ExecuteNonQuery();
                addTahsilat(tutar, aciklama, cariid);
                mesaj = true;
            }
            catch (Exception ex)
            {
                mesaj = false;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public void addSirket(string islemad, string aciklama, int tutar)
        {
            try
            {
                

                MySqlCommand comm = baglanti.CreateCommand();
                comm.CommandText = "INSERT INTO sirket(islemad, aciklama,tutar) VALUES(@islemad,@aciklama,@tutar)";
                comm.Parameters.AddWithValue("@islemad", islemad);
                comm.Parameters.AddWithValue("@aciklama", aciklama);
                comm.Parameters.AddWithValue("@tutar", tutar);
                comm.ExecuteNonQuery();
                mesaj = true;
            }
            catch (Exception ex)
            {
                mesaj = false;
            }
            finally
            {
                baglanti.Close();
            }
        }
        
        public void addTahsilat(int tutar, string aciklama, int cariid)
        {
            try
            {


                MySqlCommand comm = baglanti.CreateCommand();
                comm.CommandText = "INSERT INTO tahsilat(tutar, aciklama,cariid) VALUES(@tutar,@aciklama,@cariid)";
                comm.Parameters.AddWithValue("@tutar", tutar);
                comm.Parameters.AddWithValue("@aciklama", aciklama);
                comm.Parameters.AddWithValue("@cariid", cariid);
                comm.ExecuteNonQuery();
                mesaj = true;
            }
            catch (Exception ex)
            {
                mesaj = false;
            }
            finally
            {
                baglanti.Close();
            }

            try
            {
                baglanti.Open();
                MySqlCommand comm = baglanti.CreateCommand();
                comm.CommandText = "Update cari set tutar = tutar - @tutar where cariid = @id";
                comm.Parameters.AddWithValue("@tutar", tutar);
                comm.Parameters.AddWithValue("@id", cariid);
                comm.ExecuteNonQuery();
                mesaj = true;
            }

            catch(Exception ex)
            {
                hataMesaji = ex.Message;
            }

            finally
            {
                baglanti.Close();
            }
        }

       
        public void allMusteri()
        {
            string komut = "select * from musteri";
            da = new MySqlDataAdapter(komut, baglanti);
            dt = new DataTable();
            da.Fill(dt);

        }

        public void allCari()
        {
            string komut = "select * from cari";
            da = new MySqlDataAdapter(komut, baglanti);
            dt = new DataTable();
            da.Fill(dt);
        }

        public void allSirket()
        {
            string komut = "select * from sirket";
            da = new MySqlDataAdapter(komut, baglanti);
            dt = new DataTable();
            da.Fill(dt);
        }

        public void allTahsilat()
        {
            string komut = "select * from tahsilat";
            da = new MySqlDataAdapter(komut, baglanti);
            dt = new DataTable();
            da.Fill(dt);
        }

        public void allGelirlerGiderler()
        {
            string komut = "select * from gelirgider";
            da = new MySqlDataAdapter(komut, baglanti);
            dt = new DataTable();
            da.Fill(dt);
        }

        public void updateMusteri(int id, string tckn, string adsoyad, string telefon, string email)
        {
            try
            {
                
                MySqlCommand comm = baglanti.CreateCommand();
                comm.CommandText = "Update  musteri set tckn = @tckn , " +
                    "ad =  @ad , telefon = @telefon , email = @email" +
                    " where musteriid = @id ";
                comm.Parameters.AddWithValue("@id", id);
                comm.Parameters.AddWithValue("@tckn", tckn);
                comm.Parameters.AddWithValue("@ad", adsoyad);
                comm.Parameters.AddWithValue("@telefon", telefon);
                comm.Parameters.AddWithValue("@email", email);
                comm.ExecuteNonQuery();
                mesaj = true;
            }
            catch (Exception ex)
            {
                mesaj = false;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public void updateCari(int id, string hizmetad, string aciklama, string tarih, int tutar, int musteriid)
        {
            try
            {
                MySqlCommand comm = baglanti.CreateCommand();
                comm.CommandText = "Update  cari set hizmetad = @hizmetad , " +
                    "aciklama =  @aciklama , tarih = @tarih , tutar = @tutar , musteriid = @musteriid" +
                    " where cariid = @id ";
                comm.Parameters.AddWithValue("@id", id);
                comm.Parameters.AddWithValue("@hizmetad", hizmetad);
                comm.Parameters.AddWithValue("@aciklama", aciklama);
                comm.Parameters.AddWithValue("@tarih", tarih);
                comm.Parameters.AddWithValue("@tutar", tutar);
                comm.Parameters.AddWithValue("@musteriid", musteriid);
                comm.ExecuteNonQuery();
                mesaj = true;
            }
            catch (Exception ex)
            {
                hataMesaji = ex.Message;
                mesaj = false;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public void updateSirket(int id, string islemad, string aciklama, string tutar)
        {
            try
            {
                MySqlCommand comm = baglanti.CreateCommand();
                comm.CommandText = "Update sirket set islemad = @islemad , " +
                    "aciklama =  @aciklama , tutar = @tutar " +
                    " where sirketid = @id ";
                comm.Parameters.AddWithValue("@id", id);
                comm.Parameters.AddWithValue("@islemad", islemad);
                comm.Parameters.AddWithValue("@aciklama", aciklama);
                comm.Parameters.AddWithValue("@tutar",Convert.ToInt16( tutar));
                comm.ExecuteNonQuery();
                mesaj = true;
            }
            catch (Exception ex)
            {
                mesaj = false;
                hataMesaji = ex.Message;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public void deleteMusteri(int id)
        {
            try
            {

                MySqlCommand comm = baglanti.CreateCommand();
                comm.CommandText = "Delete From musteri where musteriid = @id ";
                comm.Parameters.AddWithValue("@id", id);
                comm.ExecuteNonQuery();
                mesaj = true;
            }
            catch (Exception ex)
            {
                mesaj = false;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public void deleteCari(int id)
        {
            try
            {

                MySqlCommand comm = baglanti.CreateCommand();
                comm.CommandText = "Delete From cari where cariid = @id ";
                comm.Parameters.AddWithValue("@id", id);
                comm.ExecuteNonQuery();
                mesaj = true;
            }
            catch (Exception ex)
            {
                mesaj = false;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public void deleteSirket(int id)
        {
            try
            {

                MySqlCommand comm = baglanti.CreateCommand();
                comm.CommandText = "Delete From sirket where sirketid = @id ";
                comm.Parameters.AddWithValue("@id", id);
                comm.ExecuteNonQuery();
                mesaj = true;
            }
            catch (Exception ex)
            {
                mesaj = false;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public void musteriDoldur()
        {
            MySqlCommand comm = baglanti.CreateCommand();
            comm.CommandText = "Select * from musteri ";
            comm.CommandType = CommandType.Text;

            MySqlDataReader dr;
            dr = comm.ExecuteReader();
            while(dr.Read())
            {
                musteriList.Add(dr["ad"]);
            }
            musteriSize = musteriList.Count; 
        }

        public DataTable musteriIdAl()
        {
            MySqlCommand comm = baglanti.CreateCommand();
            MySqlDataReader musteriOku;
            comm.CommandText = "Select musteriid, ad From musteri";
            musteriOku = comm.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(musteriOku);

            return dt;
            
        }


    }
}
