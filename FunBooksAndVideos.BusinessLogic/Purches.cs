using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Demo
{
    //public class Duck : ICanQuack
    //{
    //    public void Fly<T>(T map)
    //    {
    //        Console.WriteLine("Flying using a {0} map ({1})", typeof(T).Name, map);
    //    }

    //    public void Quack()
    //    {
    //        Console.WriteLine("Quack Quack!");
    //    }
    //}

    //public class Program
    //{
    //    //private static ICanQuack quack;
    //    private static void Main(string[] args)
    //    {
    //        SetUpQuack();

    //        var map = GetMap();

    //        quack.Fly((dynamic)map);

    //        Console.ReadKey(true);
    //    }

    //    private static void SetUpQuack()
    //    {
    //        quack = new Duck();
    //    }

    //    private static object GetMap()
    //    {
    //        return "a map";
    //    }
    //}

    public enum MembershipType
    {
        None = 0,
        BookClub = 1,
        VideoClub = 2,
        Premium = 3
    }

    public class PurchaseOrder
    {
        public int Id { get; set; }

        public decimal TotalPrice { get; set; }

        public int CustomerID { get; set; }

        public ItemLine ItemLines { get; set; }
    }

    public class ItemLine
    {
        public List<string> ProductDetails { get; set; }

        public MembershipType MembershipType { get; set; }
    }


    public interface IMembershipProcess
    {
        void ActivateMembership(PurchaseOrder purchaseOrder);
    }

    public interface IPurchaseOrderProcess
    {
        void GenerateShippingSlip(PurchaseOrder purchaseOrder);
    }


    public class PurchaseController
    {
        private readonly IPurchaseOrderProcess _purchaseOrder;
        private readonly IMembershipProcess _membership;
        public PurchaseController(IPurchaseOrderProcess purchaseOrder, IMembershipProcess membership)
        {
            this._purchaseOrder = purchaseOrder;
            this._membership = membership;
        }


        public void ProcessPurchaseOrderAsync(PurchaseOrder purchaseOrder)
        {
            if (purchaseOrder.ItemLines != null)
            {
                if (purchaseOrder.ItemLines.MembershipType != MembershipType.None)
                {
                    _membership.ActivateMembership(purchaseOrder);
                }

                if (purchaseOrder.ItemLines.ProductDetails.Any())
                {
                    _purchaseOrder.GenerateShippingSlip(purchaseOrder);
                }
            }
        }
    }
}
