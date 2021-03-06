﻿namespace CodeLibrary.ModelsMVC
{
    public interface ISoldProductAccomplishedModel
    {
        ICategoryModel Category { get; set; }
        int CategoryID { get; set; }
        int ID { get; set; }
        string Name { get; set; }
        decimal Price { get; set; }
    }
}