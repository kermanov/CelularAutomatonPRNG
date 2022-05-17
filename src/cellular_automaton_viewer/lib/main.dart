import 'dart:math';
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

  List<bool> initialState = [];
  int rule = 0;
  int length = 64;
  int iterations = 64;
  int seed = 1;

  @override
  void initState() {
    super.initState();
    initialState = List.filled(length, false);
  }

  void generateStates() {
    var newLength = length;
    var oldLength = initialState.length;
    if (newLength > oldLength) {
      initialState += List.filled(newLength - oldLength, false);
    } else if (newLength < oldLength) {
      initialState = initialState.sublist(0, newLength);
    }

    var automaton = CelularAutomaton(rule);
    states = [initialState];
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

  final Random random = Random();

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: Builder(builder: (context) {
        return Material(
          child: Stack(children: [
            InteractiveViewer(
              maxScale: 10,
              minScale: 0.1,
              child: Stack(
                children: [
                  CustomPaint(
                    painter: AutomatonPainter(states),
                    size: Size(MediaQuery.of(context).size.width,
                        MediaQuery.of(context).size.height),
                  ),
                  Row(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      for (int i = 0; i < length; ++i)
                        GestureDetector(
                          onTap: () {
                            setState(() {
                              initialState[i] = !initialState[i];
                              generateStates();
                            });
                          },
                          child: Container(
                            decoration: BoxDecoration(
                              color:
                                  initialState[i] ? Colors.black : Colors.white,
                              border: Border.all(width: 0.2),
                            ),
                            width: MediaQuery.of(context).size.width /
                                (iterations * 2),
                            height: MediaQuery.of(context).size.width /
                                (iterations * 2),
                          ),
                        )
                    ],
                  ),
                ],
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
                          "Cellular Automaton Viewer",
                          style: TextStyle(
                            fontSize: 24,
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
                          onSubmitted: (_) => {
                            setState(() {
                              generateStates();
                            }),
                          },
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
                          onChanged: (value) {
                            length = int.parse(value);
                          },
                          controller:
                              TextEditingController(text: length.toString()),
                          onSubmitted: (_) => {
                            setState(() {
                              generateStates();
                            }),
                          },
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
                          onSubmitted: (_) => {
                            setState(() {
                              generateStates();
                            }),
                          },
                        ),
                        const SizedBox(height: 8),
                        Row(
                          children: [
                            Expanded(
                              flex: 2,
                              child: ElevatedButton(
                                child: const Text("Update"),
                                onPressed: () => setState(() {
                                  generateStates();
                                }),
                              ),
                            ),
                            const SizedBox(width: 8),
                            Expanded(
                              child: OutlinedButton(
                                child: const Text("Clear"),
                                onPressed: () => setState(() {
                                  initialState = List.filled(length, false);
                                  generateStates();
                                }),
                              ),
                            )
                          ],
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
