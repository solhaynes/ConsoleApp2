namespace DeliveryProgram.Console;
using Delivery.Repository;

public class DeliveryUI
{
  private DeliveryRepository _deliveryRepo = new DeliveryRepository();

  public void Run()
  {
    SeedContent();
    Menu();
  }

  private void Menu()
  {
    bool keepRunning = true;

    while(keepRunning)
    {
      // Display options to user
      System.Console.WriteLine("Welcome to WTF's new digital delivery tracking system.");
      System.Console.WriteLine("Select a menu option: \n" +
      "1. Add a delivery to the system\n" +
      "2. Show all deliveries in the system\n" +
      "3. Show all enroute deliveries\n" +
      "4. Show all completed deliveries\n" +
      "5. Update the status of a delivery\n" +
      "6. Cancel a delivery\n" +
      "7. Exit");

      string input = System.Console.ReadLine();

      // Evaluate the user's input and act accordingly
      switch (input)
      {
        case "1": 
        // Create New Delivery
          CreateNewDelivery();
          break;
        case "2": 
        // Show all deliveries
          ShowAllDeliveries();
          break;
        case "3": 
        // Show all enroute deliveries
          ShowAllDeliveries("Enroute");
          break;
        case "4": 
        //Show all completed deliveries
          ShowAllDeliveries("Complete");
          break;
        case "5": 
        //Update delivery status
          UpdateDeliveryStatus();
          break;
        case "6": 
        //Delete existing a delivery
          DeleteCanceledDelivery();
          break;
        case "7": 
        //Exit
          System.Console.WriteLine("Goodbye!");
          keepRunning = false;
          break;
        default: 
          System.Console.WriteLine("Please enter a valid number.");
          break;
        }
      System.Console.WriteLine("Please Press any key to continue...");
      System.Console.ReadKey();
      System.Console.Clear();
    }
  }

  private void CreateNewDelivery()
  {
    System.Console.Clear();

    Delivery newDelivery = new Delivery();

    // OrderID (I added this so that I could search for a particular order in the list)
    System.Console.WriteLine("Enter the Order ID Number: ");
    int orderID = int.Parse(System.Console.ReadLine());

    // Order Date
    System.Console.WriteLine("Order Month (as a number):");
    int orderMonth = int.Parse(System.Console.ReadLine());

    System.Console.WriteLine("Order Day (as a number):");
    int orderDay = int.Parse(System.Console.ReadLine());

    System.Console.WriteLine("Order Year (as a number):");
    int orderYear = int.Parse(System.Console.ReadLine());

    DateTime orderDate = new DateTime(orderYear, orderMonth, orderDay);
    newDelivery.OrderDate = orderDate;
    System.Console.WriteLine();

    // Delivery Date
    System.Console.WriteLine("Delivery Month (as a number):");
    int deliveryMonth = int.Parse(System.Console.ReadLine());

    System.Console.WriteLine("Delivery Day (as a number):");
    int deliveryDay = int.Parse(System.Console.ReadLine());

    System.Console.WriteLine("Delivery Year (as a number):");
    int deliveryYear = int.Parse(System.Console.ReadLine());

    DateTime deliveryDate = new DateTime(deliveryYear, deliveryMonth, deliveryDay);
    newDelivery.DeliveryDate = deliveryDate;
    System.Console.WriteLine();

    // Set order status to "Scheduled" by default
    newDelivery.DeliveryStatus = "Scheduled";

    // Item number
    System.Console.WriteLine("Enter the item number:"); 
    newDelivery.ItemNumber = int.Parse(System.Console.ReadLine());

    // Item Quantity
    System.Console.WriteLine("Item quantity:");
    newDelivery.ItemQuantity = int.Parse(System.Console.ReadLine());

    // Customer ID
    System.Console.WriteLine("Customer ID: ");
    newDelivery.CustomerID = int.Parse(System.Console.ReadLine());

    _deliveryRepo.AddDeliveryToList(newDelivery);
  }

  public void ShowAllDeliveries()
  {
    var deliveryList = _deliveryRepo.GetDeliveryList();

    System.Console.WriteLine("\nAll deliveries in WTF system: ");
    foreach (Delivery delivery in deliveryList)
    {
      System.Console.WriteLine("____________________");
      System.Console.WriteLine("Order Date: " + delivery.OrderDate +
        "\nDelivery Date: " + delivery.DeliveryDate +
        "\nDelivery Status: " + delivery.DeliveryStatus +
        "\nItem Number: " + delivery.ItemNumber +
        "\nItem Quantity: " + delivery.ItemQuantity +
        "\nCustomer ID: " + delivery.CustomerID);
    }
  }
  public void ShowAllDeliveries(string deliveryStatus)
  {
    var deliveryList = _deliveryRepo.GetDeliveryList();

    System.Console.WriteLine("\nAll " + deliveryStatus + " deliveries in WTF system:");
    foreach (Delivery delivery in deliveryList)
    {
      if(delivery.DeliveryStatus == deliveryStatus)
      {
        System.Console.WriteLine("____________________");
        System.Console.WriteLine("Order ID Number: " + delivery.OrderID +
        "\nOrder Date: " + delivery.OrderDate +
        "\nDelivery Date: " + delivery.DeliveryDate +
        "\nDelivery Status: " + delivery.DeliveryStatus +
        "\nItem Number: " + delivery.ItemNumber +
        "\nItem Quantity: " + delivery.ItemQuantity +
        "\nCustomer ID: " + delivery.CustomerID);
      }
    }
  }

  private void PrintAllDeliveryIDs()
  {
    var deliveryList = _deliveryRepo.GetDeliveryList();

    System.Console.WriteLine("Order ID     Delivery Status");
    foreach(Delivery delivery in deliveryList)
    {
      System.Console.WriteLine(delivery.OrderID + "   " + delivery.DeliveryStatus);
    }
  }

  private void UpdateDeliveryStatus()
  {
    bool updated = false;

    PrintAllDeliveryIDs();

    System.Console.WriteLine("Which order would you like to update the delivery status of?");
    int userInput = int.Parse(System.Console.ReadLine());

    System.Console.WriteLine("What would you like to change the status to?\n" + 
      "1. Scheduled \n" + 
      "2. EnRoute \n" + 
      "3. Complete \n"  +
      "4. Canceled\n");
    string status = System.Console.ReadLine();

    switch(status)
    {
      case "1":
       updated = _deliveryRepo.UpdateDeliveryStatus(userInput, "Scheduled");
        break;
      case "2":
        updated = _deliveryRepo.UpdateDeliveryStatus(userInput, "EnRoute");
        break;
      case "3":
        updated = _deliveryRepo.UpdateDeliveryStatus(userInput, "Complete");
        break;
      case "4":
        updated = _deliveryRepo.UpdateDeliveryStatus(userInput, "Canceled");
        break;
      default:
        System.Console.WriteLine("Invalid selection. Try again later.");
        break;
    }
    if (updated)
    {
      System.Console.WriteLine("Status successfully updated.");
    }
    else
    {
      System.Console.WriteLine("Status couldn't be updated.");
    }
  }

  private void DeleteCanceledDelivery()
  {
    System.Console.WriteLine("In order for an order to be deleted from the system, the status must be 'Canceled'.");
    System.Console.WriteLine("Here are the available options:");

    ShowAllDeliveries("Canceled");

    System.Console.WriteLine("\nWhich canceled delivery would you like to delete?");
    int userInput = int.Parse(System.Console.ReadLine());

    bool deleted = _deliveryRepo.DeleteDeliveryFromRepository(userInput);

    if(deleted)
    {
      System.Console.WriteLine("Delivery was successfully deleted from the repository.");
    }
    else
    {
      System.Console.WriteLine("Delivery couldn't be deleted.");
    }
  }

  private void SeedContent()
  {
    DateTime orderDate1 = new DateTime(2024, 3, 15);
    DateTime orderDate2 = new DateTime(2024, 1, 12);
    DateTime orderDate3 = new DateTime(2024, 4, 5);

    DateTime deliveryDate1 = new DateTime(2024, 3, 25);
    DateTime deliveryDate2 = new DateTime(2024, 1, 30);
    DateTime deliveryDate3 = new DateTime(2024, 4, 19);

    string deliveryStatus1 = "Scheduled";
    string deliveryStatus2 = "Canceled";
    string deliveryStatus3 = "Enroute";
    string deliveryStatus4 = "Complete";

    int itemNumber1 = 12345;
    int itemNumber2 = 18273;
    int itemNumber3 = 10943;

    int itemQuantity1 = 100;
    int itemQuantity2 = 150;
    int itemQuantity3 = 96;

    int custID1 = 696969;
    int custID2 = 102932;
    int custID3 = 593762;

    Delivery delivery1 = new Delivery(1234123, orderDate1, deliveryDate1, deliveryStatus1, itemNumber1, itemQuantity1, custID1);
    Delivery delivery2 = new Delivery(4204204, orderDate2, deliveryDate2, deliveryStatus3, itemNumber2, itemQuantity2, custID2);
    Delivery delivery3 = new Delivery(6969696, orderDate3, deliveryDate3, deliveryStatus4, itemNumber1, itemQuantity3, custID3);
    Delivery delivery4 = new Delivery(2934847, orderDate1, deliveryDate3, deliveryStatus3, itemNumber3, 123, custID1);
    Delivery delivery5 = new Delivery(0947823, orderDate1, deliveryDate2, deliveryStatus4, itemNumber2, 456, custID1);

    _deliveryRepo.AddDeliveryToList(delivery1);
    _deliveryRepo.AddDeliveryToList(delivery2);
    _deliveryRepo.AddDeliveryToList(delivery3);
    _deliveryRepo.AddDeliveryToList(delivery4);
    _deliveryRepo.AddDeliveryToList(delivery5);
  }
}
