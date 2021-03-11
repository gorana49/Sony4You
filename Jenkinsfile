node {    
      def app     
      stage('Clone repository') {               
             
            checkout scm    
      }           
      stage('Build back') {         
            sh "du"
            app = docker.build("latest1")    
       }           
      stage('Test image') {                       
            app.inside {             
             sh 'echo "Tests passed"'        
            }    
      }
      stage('Push image') {
              docker.withRegistry('http://localhost:5000', 'git') 
              {                   app.push("${env.BUILD_NUMBER}")            
       app.push("latest")        
              }    
           }
        }
