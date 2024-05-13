namespace Delivery.Repository;

public class Delivery
{
  public int OrderID { get; set; }
  public DateTime OrderDate { get; set; }
  public DateTime DeliveryDate { get; set; }
  public string DeliveryStatus { get; set; }
  public int ItemNumber { get; set; }
  public int ItemQuantity { get; set; }
  public int CustomerID { get; set; }

  // Constructors
  public Delivery(){ }
  
  public Delivery(int orderID, DateTime dayOfOrder, DateTime dayOfDelivery, string statusOfDelivery, int itemNumber, int quantityOfItem, int customerID)
  {
    OrderID = orderID;
    OrderDate = dayOfOrder;
    DeliveryDate = dayOfDelivery;
    DeliveryStatus = statusOfDelivery;
    ItemNumber = itemNumber;
    ItemQuantity = quantityOfItem;
    CustomerID = customerID;
  }
}
