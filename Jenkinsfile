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
		post {
		        always {
		            echo 'This will always run'
		        }
		        success {
		            echo 'This will run only if successful'
		        }
		        failure {
		            echo 'This will run only if failed'
        		}
		        unstable {
		            echo 'This will run only if the run was marked as unstable'
        		}
		        changed {
		            echo 'This will run only if the state of the Pipeline has changed'
		            echo 'For example, if the Pipeline was previously failing but is now successful'
		        }
		    }
}
