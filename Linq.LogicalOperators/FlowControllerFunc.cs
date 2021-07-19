namespace System.Linq.Extensions {
    public class FlowControllerFunc<T, R>: FlowController<T> {
        internal FlowControllerFunc() {
        }

        internal R MapData { get; set; }
    }
}
