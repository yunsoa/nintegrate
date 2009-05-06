dim fso
dim orderFile
dim file
dim stream
dim fileName
dim folder
dim folders
dim orderList
dim intReturn
dim rootPath
dim outputFile

folders = Array("Tables", "Stored Procedures", "Scripts")

Set fso = CreateObject("Scripting.FileSystemObject")

rootPath = fso.GetFile(Wscript.ScriptFullName).ParentFolder.Path

on error resume next
Set objDialog = CreateObject("SAFRCFileDlg.FileSave")
objDialog.FileName = rootPath & "\NIntegrateConfigAllInOneSetupScript.sql"
objDialog.FileType = "Sql Script"
intReturn = objDialog.OpenFileSaveDlg

If intReturn Then
	Set outputFile = fso.CreateTextFile(objDialog.FileName, true)
elseif not objDialog then
    Set outputFile = fso.CreateTextFile(rootPath & "\NIntegrateConfigAllInOneSetupScript.sql", true)
else
    msgbox "Error occured!"
end if
	
If outputFile Then	
	orderList = ""
	for each folder in folders
		if fso.FileExists(rootPath & "\" & folder & "\Order.txt") then
			Set orderFile = fso.OpenTextFile(rootPath & "\" & folder & "\Order.txt", 1, true, 0)
			while not orderFile.AtEndOfStream
				fileName = orderFile.ReadLine()
				if lcase(right(fileName, 4)) <> ".txt" and fso.FileExists(rootPath & "\" & folder & "\" & fileName) then
					orderList = orderList & fileName & ","
					'outputFile.WriteLine("USE [" & left(fileName, instr(fileName, ".") - 1) & "]")
					set stream =  CreateObject("ADODB.Stream")
					stream.Type = 2
					stream.Charset = "UTF-8"
					stream.Open() 
					stream.LoadFromFile(rootPath & "\" & folder & "\" & fileName) 
					outputFile.WriteBlankLines(1)
					outputFile.WriteLine(stream.ReadText())
					outputFile.WriteBlankLines(2)
					stream.close()
				end if
			wend
			orderFile.close()
		end if

		for each file in fso.GetFolder(rootPath & "\" & folder).Files
			if lcase(right(file.Name, 4)) <> ".txt" and instr(orderList, file.Name) <= 0 then
				'outputFile.WriteLine("USE [" & left(file.Name, instr(file.Name, ".") - 1) & "]")
				set stream =  CreateObject("ADODB.Stream")
				stream.Type = 2
				stream.Charset = "UTF-8"
				stream.Open() 
				stream.LoadFromFile(file.Path) 
				outputFile.WriteBlankLines(1)
				outputFile.WriteLine(stream.ReadText())
				outputFile.WriteBlankLines(2)
				stream.close()				
			end if
		next
		outputFile.WriteLine("GO")
	next
	
	outputFile.close()
Else
    Wscript.Quit
End If