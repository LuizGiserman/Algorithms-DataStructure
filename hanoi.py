#hanoi boys


def tower (disk, source, intermediate, destination):
        global x

        if (disk == 1):
            destination.append(source.pop())
            #next two lines are merely to print out the steps
            x+=1
            print ("\n#{}\n{}\n{}\n{}\n".format(x, source, intermediate, destination))
            return
        else:
            tower(disk - 1, source, destination, intermediate)
            destination.append(source.pop())
            #next two lines to print out the steps
            x +=1
            print ("\n#{}\n{}\n{}\n{}\n".format(x, source, destination, intermediate))
            tower(disk - 1, intermediate, source, destination)

x = 0;
n = int (input ("Enter n: "))
source = [disk for disk in range (n, 0, -1)]
intermediate = []
destination = []

#printing the initial state
x +=1
print ("\n#{}\n{}\n{}\n{}\n".format(x, source, intermediate, destination))

tower(len(source), source, intermediate, destination)
