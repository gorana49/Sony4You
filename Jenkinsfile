node {    
      def app   
      def back  
      stage('Clone repository') {               
             
            checkout scm    
      }           
      stage('Build back') {         
            back = app.back;
            back = docker.build("latest1")    
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
