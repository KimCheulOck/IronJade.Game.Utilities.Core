using IronJade.Util.Core;

public class ConvenienceModel
{
    public static JsonConvenienceModel Json { get { return jsonUtil == null ? jsonUtil = new JsonConvenienceModel() : jsonUtil; } }
    public static CompareConvenienceModel Compare { get { return compareUtil == null ? compareUtil = new CompareConvenienceModel() : compareUtil; } }
    public static StringConvenienceModel String { get { return stringUtil == null ? stringUtil = new StringConvenienceModel() : stringUtil; } }
    public static TimeConvenienceModel Time { get { return timeUtil == null ? timeUtil = new TimeConvenienceModel() : timeUtil; } }
    public static CurveConvenienceModel Curve { get { return curveUtil == null ? curveUtil = new CurveConvenienceModel() : curveUtil; } }
    public static MathConvenienceModel Math { get { return mathUtil == null ? mathUtil = new MathConvenienceModel() : mathUtil; } }
    public static RandomConvenienceModel Random { get { return randomUtil == null ? randomUtil = new RandomConvenienceModel() : randomUtil; } }

    private static JsonConvenienceModel jsonUtil = null;
    private static CompareConvenienceModel compareUtil = null;
    private static StringConvenienceModel stringUtil = null;
    private static TimeConvenienceModel timeUtil = null;
    private static CurveConvenienceModel curveUtil = null;
    private static MathConvenienceModel mathUtil = null;
    private static RandomConvenienceModel randomUtil = null;
}
