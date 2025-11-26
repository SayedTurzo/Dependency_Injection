using Reflex.Attributes;
using UnityEngine;
using Reflex.Core;
using Reflex.Injectors; // <--- 1. ADD THIS NAMESPACE

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    private Container _container;

    [Inject]
    private void Construct(Container container)
    {
        _container = container;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpawnBullet();
        }
    }

    void SpawnBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        GameObjectInjector.InjectRecursive(bullet, _container);
    }
}
