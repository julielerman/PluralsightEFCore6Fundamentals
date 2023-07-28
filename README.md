# Pluralsight EF Core 6 Fundamentals  
All code samples for [EF Core 6 Fundamentals on Pluralsight](https://pluralsight.pxf.io/EFCore6)  
Published May 3, 2022  

Samples are organized by module with before and after versions. The last 3 modules (14, 15, 16) don't have before and after. They just have solutions that were discussed in those modules.

Solutions were built using Visual studio 2022 on Windows and using SQL Server LocalDb.

There are two branches. EFCore6 (original in course) and EFCore7.

The EF Core 7 branch is still using .NET6 but every solution (before and after) has been updated to target EF Core 7.

The demos for Modules 2-13 required NO CHANGES AT ALL. 

I had to make one change in the API Testing demo (also part of M13) which was to call SaveChanges when adding data to the inmemory context.
I could not find details about this change on GitHub. But also want to note that team is more adamant about saying the the provider is not recommended,
so for the udpate to the course (EF Core 8) I will not use it any more.

