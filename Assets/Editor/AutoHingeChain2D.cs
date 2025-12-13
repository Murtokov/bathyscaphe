using UnityEditor;
using UnityEngine;

public static class AutoHingeChain2D
{
    [MenuItem("Tools/2D/Auto Create HingeJoints (Selected Root)")]
    public static void Create()
    {
        var root = Selection.activeGameObject;
        if (root == null)
        {
            Debug.LogError("Select root bone GameObject in Hierarchy.");
            return;
        }

        Undo.RegisterFullObjectHierarchyUndo(root, "Auto Create Hinge Chain 2D");

        // 1) Убедимся что у root есть Rigidbody2D
        var rootRb = GetOrAdd<Rigidbody2D>(root);
        SetupBodyDefaults(rootRb);

        // 2) Пройдёмся по всем трансформам (включая root) и соединим каждого с родителем
        var all = root.GetComponentsInChildren<Transform>(true);
        foreach (var t in all)
        {
            if (t == root.transform) continue;
            if (t.parent == null) continue;

            var go = t.gameObject;

            // Rigidbody2D на кость
            var rb = GetOrAdd<Rigidbody2D>(go);
            SetupBodyDefaults(rb);

            // Rigidbody2D на родителя (на всякий)
            var parentRb = GetOrAdd<Rigidbody2D>(t.parent.gameObject);
            SetupBodyDefaults(parentRb);

            // HingeJoint2D на дочернюю кость
            var hj = GetOrAdd<HingeJoint2D>(go);
            hj.autoConfigureConnectedAnchor = false;
            hj.connectedBody = parentRb;

            // Точка стыка: берём позицию самой дочерней кости (обычно это и есть “сустав”)
            // anchor — в локальных координатах ребёнка
            // connectedAnchor — в локальных координатах родителя
            Vector3 jointWorld = t.position;

            Vector2 anchorLocal = t.InverseTransformPoint(jointWorld);
            Vector2 connectedAnchorLocal = t.parent.InverseTransformPoint(jointWorld);

            hj.anchor = anchorLocal;
            hj.connectedAnchor = connectedAnchorLocal;

            // Часто полезно:
            hj.enableCollision = false; // чтобы соседние кости не дрались коллайдерами (если они есть)

            // Лимиты (можешь подстроить под себя)
            hj.useLimits = true;
            var lim = hj.limits;
            lim.min = -25f;
            lim.max = 25f;
            hj.limits = lim;

            EditorUtility.SetDirty(go);
        }

        Debug.Log($"Done: processed {all.Length} transforms under '{root.name}'.");
    }

    private static void SetupBodyDefaults(Rigidbody2D rb)
    {
        // Не переопределяем, если ты уже настроил вручную — но можно ужесточить при желании
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0f; // для “костей” часто гравитацию делают 0 и тянут силой/целью
        rb.angularDrag = 0.2f;
        rb.drag = 0.0f;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    private static T GetOrAdd<T>(GameObject go) where T : Component
    {
        var c = go.GetComponent<T>();
        if (c == null) c = Undo.AddComponent<T>(go);
        return c;
    }
}