namespace Delivery.Repository;

public class DeliveryRepository
{
  private List<Delivery> _deliveryList = new List<Delivery>();

  // Create
  public void AddDeliveryToList(Delivery delivery)
  {
    _deliveryList.Add(delivery);
  }
  // Read
  public List<Delivery> GetDeliveryList()
  {
    return new List<Delivery>(_deliveryList);
  }
  // Update
  public bool UpdateDeliveryStatus(int orderID, string status)
  {
    Delivery delivery = GetDeliveryByOrderID(orderID);

    if (delivery != null)
    {
      delivery.DeliveryStatus = status;

      return true;
    }
    else
    {
      return false;
    }
  }
  // Delete
  public bool DeleteDeliveryFromRepository(int orderID)
  {
    Delivery delivery = GetDeliveryByOrderID(orderID);

    if(delivery == null)
    {
      return false;
    }

    int initialCount = _deliveryList.Count;
    _deliveryList.Remove(delivery);

    if (initialCount > _deliveryList.Count)
    {
      return true;
    }
    else
    {
      return false;
    }
  }

  // Helper method
  public Delivery GetDeliveryByOrderID(int orderID)
  {
    foreach(Delivery delivery in _deliveryList)
    {
      if(delivery.OrderID == orderID)
      {
        return delivery;
      }
    }
    return null;
  }
}

