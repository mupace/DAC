dotnet-ef dbcontext scaffold "Name=ConnectionStrings:DefaultConnection" "Microsoft.EntityFrameworkCore.SqlServer" -p "..\Data\DAC.DB" -o "Models"  -n "DAC.DB.Models" --force