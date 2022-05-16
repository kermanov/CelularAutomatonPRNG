class CelularAutomaton {
  int rule;

  CelularAutomaton(this.rule);

  List<bool> getNextState(List<bool> currentState) {
    var nextState = <bool>[];

    for (int i = 0; i < currentState.length; ++i) {
      var pattenIndex =
          (currentState[(i - 1 + currentState.length) % currentState.length]
                  ? 1
                  : 0) +
              (currentState[i] ? 2 : 0) +
              (currentState[(i + 1) % currentState.length] ? 4 : 0);

      nextState.add(((1 << pattenIndex) & rule) > 0);
    }
    return nextState;
  }
}
