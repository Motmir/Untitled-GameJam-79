using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class LASER : ParentBullet
{
    Vector3 StartVector, FinishVector, AdjustVector;
    float durationStart;
    
    public void setFinishVector(Vector3 Finish){ FinishVector = Finish; }

    public override void Setup(Vector3 Start)
    {
        Debug.Log("LASEEEEER");
        StartVector = Start;
        rb = transform.GetComponent<Rigidbody2D>();
        float rotation = Mathf.Rad2Deg * Mathf.Atan2(Start.y, Start.x);
        gameObject.transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
        durationStart = duration;
    }

    public void adjustBeam()
    {
        
        float progress = ((durationStart - duration) / durationStart);   
        
        AdjustVector = Vector3.Lerp(StartVector, FinishVector, progress);
        rb = transform.GetComponent<Rigidbody2D>();

        float rotation = Mathf.Rad2Deg * Mathf.Atan2(AdjustVector.y, AdjustVector.x);
        gameObject.transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
    }

    public override void FixedUpdate()
    {
        duration -= Time.deltaTime;
        adjustBeam();
        if (duration < 0)
        {
            Destroy(gameObject);
        }
    }

    public override void CustomDelAnim()
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Spaceship")
        {
            GameObject.Find("GameMaster").GetComponent<GameMaster>().DecreaseCows();
            GameObject.Find("Spaceship").GetComponent<Rigidbody2D>().AddForce(AdjustVector);
            CustomDelAnim();
            Destroy(gameObject);
        }
    }

}
