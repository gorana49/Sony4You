node {    
      def app   
      stage('Clone repository') {               
             
            checkout scm    
      }           
      stage('Build back') {       
            app = docker.build("my-image -f ./back")
       }           
      stage('Test image') {                       
            app.inside {             
             sh 'echo "Tests passed"'        
            }    
      }
      stage('Push image') {
              docker.withRegistry('http://localhost:5000') 
              {                   app.push("${env.BUILD_NUMBER}")            
       app.push("latest")        
              }    
           }
        }
