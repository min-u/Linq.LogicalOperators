namespace System.Linq.Extensions {
    public static class LogicalOperators {
        /// <summary>
        /// Labda 논리연산자: If
        /// <pra>논리조건의 시작</pra>
        /// <para>주의: If.Then.If 순으로 사용하면 의도치 않은 동작이 발생할 수 있습니다.</para>
        /// <para>If.Then.ElseIf.Then.ElseIf.Then.Else.End 와 같은 순으로 사용을 해야 합니다.</para>
        /// </summary>
        public static FlowController<T> If<T>(this T source, Predicate<T> predicate)
            => new FlowController<T> {
                True = predicate(source),
                OriginSource = source,
            };

        public static FlowController<T> ElseIf<T>(this FlowController<T> source, Predicate<T> predicate) {
            source.True = predicate(source.OriginSource);
            return source;
        }

        public static FlowController<T> Then<T>(this FlowController<T> source, Action thenAction) {
            if(source.True)
                thenAction();

            return source;
        }

        public static FlowControllerFunc<T, R> ThenMap<T, R>(this FlowController<T> source, Func<T, R> thenAction) {
            var mapConditional = new FlowControllerFunc<T, R>() {
                True = source.True,
                OriginSource = source.OriginSource,
            };

            if(source.True)
                mapConditional.MapData = thenAction(source.OriginSource);

            return mapConditional;
        }

        public static FlowController<T> ElseMap<T>(this FlowController<T> source, Action elseFunc) {
            if(!source.True)
                elseFunc();

            return source;
        }

        public static FlowControllerFunc<T, R> ElseMap<T, R>(this FlowController<T> source, Func<T, R> wlseFunc) {
            var mapConditional = new FlowControllerFunc<T, R>() {
                True = source.True,
                OriginSource = source.OriginSource,
            };

            if(!source.True)
                mapConditional.MapData = wlseFunc(source.OriginSource);

            return mapConditional;
        }

        public static void End<T>(this FlowController<T> _) { }

        public static R End<T, R>(this FlowControllerFunc<T, R> source)
            => source.MapData;
    }
}
