using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 0.1f,radius=0.2f,damage=1; // �ӵ�,�Ѿ� ������, ���ط�
    public LayerMask layerMask;
    [SerializeField]
    internal float _maxTime; //�ִ� LifeTime (���� �� �� �ð��� ������ �Ѿ� �����)
    internal float _time;
    internal RaycastHit _hit;
    public ProjectileType bulletType;
    public TrailRenderer trailRenderer;
    public bool Pls = false;

    public virtual void  OnEnable()
    {
        _time = 0; // �Ѿ� LifeTime �ʱ�ȭ
        Mov();
        trailRenderer.Clear();
        Mov();
    }

    public virtual void OnDisable()
    {
        trailRenderer.Clear();
    }

    public virtual void FixedUpdate()
    {
        Mov();
    }
    public void Mov()
    {
        if (Physics.SphereCast(transform.position, radius, transform.forward, out _hit, speed, layerMask,QueryTriggerInteraction.Collide))
        {
            if (_hit.transform.gameObject.CompareTag("Hitable") || _hit.transform.gameObject.CompareTag("Head"))
            {
                Transform Cum = _hit.transform.gameObject.transform;
                Health hpsdf;

                while (!Cum.TryGetComponent<Health>(out hpsdf))
                {
                    Cum = Cum.parent;
                }
                if (_hit.transform.gameObject.CompareTag("Head"))
                {
                    hpsdf.Damage(damage * 2);
                }
                else
                {
                    hpsdf.Damage(damage);
                }
            }

            GameMana.instance.pool.Get(bulletType, gameObject); // �Ѿ� Ǯ�� ���ֱ� ����
        }
        transform.position += transform.forward * speed; //�Ѿ� �̵�(���̷� �� ������ ������ �̵�)
        _time += Time.fixedDeltaTime;

        if (_time >= _maxTime)
        {
            GameMana.instance.pool.Get(bulletType, gameObject); // �Ѿ� Ǯ�� ��
        }
    }
}
