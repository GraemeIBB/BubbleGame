using UnityEngine;

public class Player : Entity
{
    
    void start(){
        base.Start();
    }


    void Update(){
        base.Update();
        if(Input.GetKeyDown(KeyCode.Space)){
            base.jumpCommand = true;
        }
    }
    

}
