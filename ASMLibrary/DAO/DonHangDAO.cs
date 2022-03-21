using ASMLibrary.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMLibrary.DAO
{
    public class DonHangDAO
    {
        public DonHangDAO() { }
        public IEnumerable<DonHang> GetDonHangList()
        {
            List<DonHang> donHangs;
            try
            {
                var ASMFDB = new ASMFContext();
                var temp = from i in ASMFDB.DonHangs where i.Tt == 1 select i;
                donHangs = temp.ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return donHangs;
        }
        public IEnumerable<DonHang> GetDonHangByIDKhachHang(String id)
        {
            List<DonHang> donHangs;
            try
            {
                var ASMFDB = new ASMFContext();
                
                donHangs = ASMFDB.DonHangs.Where(g => g.Idkh.Equals(id)).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return donHangs;
        }
        public DonHang GetDonHangByID(String id)
        {
            DonHang donHang = null;
            try
            {
                var ASMFDB = new ASMFContext();
                donHang = ASMFDB.DonHangs.SingleOrDefault(m => m.IddonHang.Equals(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return donHang;
        }
        public String GetIDCuoi()
        {
            List<DonHang> donHangs;

            try
            {
                var ASMFDB = new ASMFContext();
                donHangs = ASMFDB.DonHangs.Select((DonHang i) => i).ToList();
                if (donHangs.Count <= 0)
                {
                    return "D0001";
                }
                string iDCuoi = donHangs.Last().IddonHang;

                return $"D{int.Parse(iDCuoi.Substring(1)) + 1:000#}";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
        public void AddDonHang(DonHang donHang)
        {
            try
            {
                DonHang _donHang = GetDonHangByID(donHang.IddonHang);
                if (_donHang == null)
                {
                    var ASMFDB = new ASMFContext();
                    ASMFDB.DonHangs.Add(donHang);
                    ASMFDB.SaveChanges();
                }
                else
                {
                    throw new Exception("Order is already exist.");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateDonHang(DonHang donHang)
        {
            try
            {
                DonHang _donHang = GetDonHangByID(donHang.IddonHang);
                if (_donHang != null)
                {
                    var ASMFDB = new ASMFContext();
                    ASMFDB.Entry<DonHang>(donHang).State = EntityState.Modified;
                    ASMFDB.SaveChanges();
                }
                else
                {
                    throw new Exception("Order does not exist.");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteDonHang(DonHang donHang)
        {
            try
            {
                DonHang _donHang = GetDonHangByID(donHang.IddonHang);
                if (_donHang != null)
                {
                    donHang.Tt = 0;
                    var ASMFDB = new ASMFContext();
                    ASMFDB.Entry<DonHang>(donHang).State = EntityState.Modified;
                    ASMFDB.SaveChanges();
                }
                else
                {
                    throw new Exception("Order does not exist.");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
