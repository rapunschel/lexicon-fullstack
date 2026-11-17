using System;

namespace StorageAPI.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public string Catgory { get; set; }
    public string Shelf { get; set; }
    public int Count { get; set; }
    public string Description { get; set; }
}
