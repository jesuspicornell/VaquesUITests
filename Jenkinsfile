pipeline {
	agent any
	stages {
		stage("compilar") {
		steps	{
			echo "Compilant..."
			}
		}
		
		stage("test") {
		steps	{
			echo "Testejant..."
			dir("/home/jpicornell/dia4/VaquesUITests") {
				sh "docker-compose up -d"
				sleep 30
				}
			
			}
			
			dir("/home/jpicornell/dia4/VaquesUITests/vaquesUiTest") {
				sh "dotnet test"
				
				}
			
			}
		}
	}
}
