import subprocess, threading

seed = 123

def run(rule: int):
    command = f'rand.exe -k 128 -r {rule} -s {seed} -f results/rule_{rule}_seed_{seed}.bin'
    process = subprocess.Popen(command.split())
    process.wait()

    command = f'python D:/university/CelularAutomatonPRNG/src/sp800_22_tests/sp800_22_tests.py results/rule_{rule}_seed_{seed}.bin'
    process = subprocess.Popen(command.split(), stdout=subprocess.PIPE)
    with open(f"test_results/rule_{rule}_seed_{seed}.txt", "w+") as file:
        for line in iter(process.stdout.readline, b''):
            file.write(line.decode())

    print(f"Rule {rule} done.")



threadsCount = 3
rulesToCheck = 3
startRule = 253
for i in range(startRule, startRule + rulesToCheck, threadsCount):
    threads = [threading.Thread(target=run, args=(i + t,)) for t in range(threadsCount)]
    for thread in threads:
        thread.start()
    for thread in threads:
        thread.join()

print("All done.")
