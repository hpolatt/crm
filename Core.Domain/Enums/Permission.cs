namespace Core.Domain.Enums;

public enum Permission
{
    // User Permissions
        ViewUsers = 1100,
        CreateUsers = 1200,
        UpdateUsers = 1300,
        DeleteUsers = 1400,
        
        // Order Permissions
        ViewOrders = 2100,
        CreateOrders = 2200,
        UpdateOrders = 2300,
        DeleteOrders = 2400,
        
        // Customer Permissions
        ViewCustomers = 3100,
        CreateCustomers = 3200,
        UpdateCustomers = 3300,
        DeleteCustomers = 3400


    
}
