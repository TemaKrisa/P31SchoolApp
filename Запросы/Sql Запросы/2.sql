Select p.Surname[Фамилия], p.[Name],p.Midname[Отчество] From Grade g join Pupil p on g.PupilID = p.PupilID  Where p.ClassID = 1 Or p.ClassID = 2
Select Surname[Фамилия], [Name], Midname[Отчество] From Pupil Where Not Surname = 'Громов'
Select Surname[Фамилия], [Name], Midname[Отчество] From Pupil Where Surname = 'Чернышёва' and Midname = 'Мироновна'