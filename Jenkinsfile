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
                        sh "pwd ; ls" 
			sh "docker run hello-world"
			sh "docker-compose up -d"
			sleep 30
			
			
			dir("vaquesUiTest") {
				sh "dotnet test"
				
				}
			
			}
		}
	}
}
