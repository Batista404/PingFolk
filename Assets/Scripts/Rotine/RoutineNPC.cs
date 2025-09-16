using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class RoutineNPC : MonoBehaviour
{
    [SerializeField] GameObject Countainer;
    [SerializeField] List<Transform> Positions;
    [SerializeField] Transform NextPosition;
    [SerializeField] int Speed;

    [SerializeField] NPCMultiDi NpcDialogueSys;

    [SerializeField] bool IsReverse, SideGoing;

    [SerializeField] int IdPosition;

    private void Start()
    {
        NpcDialogueSys = GetComponent<NPCMultiDi>();

        // 1. Conseguir os transform de cada item dentro do Countainer
        for(int i = 1; i < Countainer.GetComponentsInChildren<Transform>().Length; i++)
        {
            Positions.Add(Countainer.GetComponentsInChildren<Transform>()[i]);
        }
        NextPosition = Positions[IdPosition];
    }
    private void Update()
    {
        // 2. Conseguir o primeiro item da lista
        // 3. Colocar o valor em next Position

        // 4. Move na direção
        if (!NpcDialogueSys.InDialogue)
        {
            transform.position = Vector3.MoveTowards(
                current: transform.position,
                target: NextPosition.position,
                maxDistanceDelta: Speed * Time.deltaTime
            );
            
            Vector3 direction = NextPosition.position - transform.position;
            if (direction.sqrMagnitude > 0.0001f)
            {
                Quaternion targetRot = Quaternion.LookRotation(direction.normalized);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, (Speed*25) * Time.deltaTime);
            }
        }
        else
        {
            return;
        }

        // 5. quando for a mesma posição, ir para o proximo item da lista E se for o último voltar para o primeiro
        if (IdPosition >= Positions.Count)
        {
            if (IsReverse)
            {
                IdPosition--;
                SideGoing = true; //ir ao contrario
            }
            else
            {
                IdPosition = 0;

            }
            ChangeNextPo();
        }
        else if (transform.position == NextPosition.position)
        {

            if (SideGoing)
            {
                IdPosition--;
            }
            else
            {
                IdPosition++;

            }
            ChangeNextPo();
        }
        else if (IdPosition == 0 && IsReverse)
        {
            SideGoing = false;
        }
        //Debug.Log("Positions " + Positions.Count);
    }

    void ChangeNextPo()
    {
        if (Positions == null || Positions.Count == 0) return;
        if (IdPosition < 0 || IdPosition >= Positions.Count) return;

        NextPosition = Positions[IdPosition];
    }
}
