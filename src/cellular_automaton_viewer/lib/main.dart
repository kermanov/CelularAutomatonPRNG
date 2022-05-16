import 'dart:ui';

import 'package:cellular_automaton_viewer/celular_automaton.dart';
import 'package:cellular_automaton_viewer/custom_painter.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatefulWidget {
  const MyApp({Key? key}) : super(key: key);

  @override
  State<MyApp> createState() => _MyAppState();
}

class _MyAppState extends State<MyApp> {
  List<List<bool>> states = [];

  int rule = 0;
  int length = 64;
  int iterations = 64;
  int seed = 1;

  void generateStates() {
    var automaton = CelularAutomaton(rule);

    states = [];
    var firstState = getIntBits(seed);
    if (firstState.length < length) {
      firstState.insertAll(
          0, List<bool>.filled(length - firstState.length, false));
    } else if (firstState.length > length) {
      firstState = firstState.sublist(firstState.length - length);
    }
    states.add(firstState);

    for (int i = 1; i < iterations; ++i) {
      states.add(automaton.getNextState(states.last));
    }
  }

  List<bool> getIntBits(int number) {
    var bits = <bool>[];
    for (int i = 0; i < 64; ++i) {
      bits.add(((1 << i) & number) > 0);
    }
    return bits.reversed.toList();
  }

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: Builder(builder: (context) {
        return Material(
          child: Stack(children: [
            InteractiveViewer(
              maxScale: 10,
              minScale: 0.1,
              // child: Column(
              //     crossAxisAlignment: CrossAxisAlignment.stretch,
              //     children: states
              //         .map((state) => Expanded(
              //               child: Row(
              //                 mainAxisSize: MainAxisSize.min,
              //                 mainAxisAlignment: MainAxisAlignment.center,
              //                 children: state
              //                     .map((bit) => AspectRatio(
              //                           aspectRatio: 1,
              //                           child: Container(
              //                               color: bit
              //                                   ? Colors.black
              //                                   : Colors.white),
              //                         ))
              //                     .toList(),
              //               ),
              //             ))
              //         .toList()),

              // child: ListView.builder(
              //   itemCount: states.length,
              //   itemBuilder: (context, index) {
              //     var state = states[index];
              //     var string = state.map((e) => e ? "⬛" : " ").join();
              //     return Text(
              //       string,
              //       style: TextStyle(
              //         fontFeatures: [FontFeature.tabularFigures()],
              //       ),
              //     );
              //   },
              // ),

              child: CustomPaint(
                painter: AutomatonPainter(states),
                size: Size(MediaQuery.of(context).size.width,
                    MediaQuery.of(context).size.height),
              ),
            ),
            Row(
              children: [
                const Spacer(flex: 4),
                Expanded(
                  child: Container(
                    color: Colors.white.withAlpha(240),
                    padding: const EdgeInsets.all(8),
                    child: Column(
                      mainAxisSize: MainAxisSize.min,
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        const Text(
                          "Controls",
                          style: TextStyle(
                            fontSize: 32,
                            fontWeight: FontWeight.bold,
                          ),
                        ),
                        const SizedBox(height: 8),
                        TextField(
                          decoration: const InputDecoration(
                            border: OutlineInputBorder(),
                            labelText: 'Rule',
                          ),
                          keyboardType: TextInputType.number,
                          inputFormatters: [
                            FilteringTextInputFormatter.digitsOnly
                          ],
                          onChanged: (value) => rule = int.parse(value),
                          controller:
                              TextEditingController(text: rule.toString()),
                        ),
                        const SizedBox(height: 8),
                        TextField(
                          decoration: const InputDecoration(
                            border: OutlineInputBorder(),
                            labelText: 'Seed',
                          ),
                          keyboardType: TextInputType.number,
                          inputFormatters: [
                            FilteringTextInputFormatter.digitsOnly
                          ],
                          onChanged: (value) => seed = int.parse(value),
                          controller:
                              TextEditingController(text: seed.toString()),
                        ),
                        const SizedBox(height: 8),
                        TextField(
                          decoration: const InputDecoration(
                            border: OutlineInputBorder(),
                            labelText: 'Lenght',
                          ),
                          keyboardType: TextInputType.number,
                          inputFormatters: [
                            FilteringTextInputFormatter.digitsOnly
                          ],
                          onChanged: (value) => length = int.parse(value),
                          controller:
                              TextEditingController(text: length.toString()),
                        ),
                        const SizedBox(height: 8),
                        TextField(
                          decoration: const InputDecoration(
                            border: OutlineInputBorder(),
                            labelText: 'Iterations',
                          ),
                          keyboardType: TextInputType.number,
                          inputFormatters: [
                            FilteringTextInputFormatter.digitsOnly
                          ],
                          onChanged: (value) => iterations = int.parse(value),
                          controller: TextEditingController(
                              text: iterations.toString()),
                        ),
                        const SizedBox(height: 8),
                        OutlinedButton(
                          child: const Text("Generate"),
                          onPressed: () => setState(() {
                            generateStates();
                          }),
                        ),
                      ],
                    ),
                  ),
                ),
              ],
            ),
          ]),
        );
      }),
    );
  }
}
