import 'dart:html';

import 'package:flutter/material.dart';

class AutomatonPainter extends CustomPainter {
  final List<List<bool>> states;

  AutomatonPainter(this.states);

  @override
  void paint(Canvas canvas, Size size) {
    if (states.isNotEmpty) {
      var rectPaint = Paint();
      rectPaint.style = PaintingStyle.fill;

      var width = size.width / 2;
      var rectSize = width / states.length;
      var left = size.width / 2 - states[0].length * rectSize / 2;

      for (int i = 0; i < states.length; ++i) {
        for (int k = 0; k < states[i].length; ++k) {
          rectPaint.color = states[i][k] ? Colors.black : Colors.white;

          canvas.drawRect(
              Rect.fromLTWH(
                  left + k * rectSize, i * rectSize, rectSize, rectSize),
              rectPaint);
        }
      }
    }
  }

  @override
  bool shouldRepaint(covariant CustomPainter oldDelegate) {
    return false;
  }
}
