Select ClassName[Класс] from Class where ClassName like '%Б'
Select Distinct ClassName from Schedule s join Class c on s.ClassID = c.ClassID  Where DayOfTheWeek like 'Понедельник'