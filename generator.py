import math


def main():
    k = 126.942 # one full round
    angle = 15 # angle in degree
    speedOfFiber = 11000 # mm/min speed of fiber
    diameter = 85 # mm diameter of tube
    L = 600 # mm length of tube
    angleOfTurn = 360 # deg angle of turn after rotation
    fiberWidth = 6 # mm width of fiber

    s =  math.pi*diameter*math.tan(math.radians(angle)) # x move for one turn
    rotation = L*k/s
    F_forMove = speedOfFiber*math.sqrt(k*k + s*s)/math.sqrt(math.pi*diameter*math.pi*diameter + s*s) # check this
    Fy = k*F_forMove/math.sqrt(k*k + s*s)
    trueFiberWidth = fiberWidth/math.sin(math.radians(angle))
    angleAddition = math.degrees(2*trueFiberWidth/diameter)
    fullRoundRotation = rotation*2
    countOfLoops = fullRoundRotation/k
    addition = round(countOfLoops)-countOfLoops

    countRepeat = round(360/angleAddition + .5)
    print(s, F_forMove, Fy, rotation, angleAddition, 360/angleAddition, trueFiberWidth, countRepeat)
    with open("result.gcode", "w") as file:
        file.write("G21\n")
        file.write("G91\n")
        file.write(f'G01 Y-{k*2} F{Fy:.3f}\n') 

        for i in range(countRepeat):
            file.write(f'G01 X{L} Y-{rotation:.3f} F{F_forMove:.3f}\n')
            file.write(f'G01 Y-{2*k:.3f} F{Fy:.3f}\n')
            file.write(f'G01 X-{L} Y-{rotation:.3f} F{F_forMove:.3f}\n')
            file.write(f'G01 Y-{(((2*360+addition*360 + angleAddition)*k)/360):.3f} F{Fy:.3f}\n')

if __name__ == "__main__":
    main()