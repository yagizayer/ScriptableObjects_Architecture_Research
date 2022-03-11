namespace Helpers
{
    public enum Vector3Values
    {
        X,
        Y,
        Z,
        XY,
        XZ,
        YZ,
        XYZ
    }

    public enum RectValues
    {
        X,
        Y,
        Width,
        Height
    }

    public enum ColorValues
    {
        R,
        G,
        B,
        A
    }

    public enum RectFields
    {
        None = 0,
        X = 1,
        Y = 2,
        Width = 4,
        Height = 8
    }

    
    public enum Pivot3D
    {
        BottomBackLeft,
        BottomBackCenter,
        BottomBackRight,
        BottomCenterLeft,
        BottomCenterCenter,
        BottomCenterRight,
        BottomFrontLeft,
        BottomFrontCenter,
        BottomFrontRight,
        CenterBackLeft,
        CenterBackCenter,
        CenterBackRight,
        CenterCenterLeft,
        CenterCenterCenter,
        CenterCenterRight,
        CenterFrontLeft,
        CenterFrontCenter,
        CenterFrontRight,
        TopBackLeft,
        TopBackCenter,
        TopBackRight,
        TopCenterLeft,
        TopCenterCenter,
        TopCenterRight,
        TopFrontLeft,
        TopFrontCenter,
        TopFrontRight
    }

    public enum UnitySpecificEvents
    {
        Null = 0,
        Awake = 10,
        Start = 20,
        Update = 30,
        FixedUpdate = 40,
        LateUpdate = 50,
        OnEnable = 60,
        OnDisable = 70,
        OnDestroy = 80,
        OnTriggerEnter = 90,
        OnTriggerStay = 100,
        OnTriggerExit = 110,
        OnCollisionEnter = 120,
        OnCollisionStay = 130,
        OnCollisionExit = 140,
        
    }
}
