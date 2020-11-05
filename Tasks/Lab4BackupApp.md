## Backup App
### Laboratory work №4

### [Solution](https://github.com/annchous/OopLabs/tree/lab4/OopLabs/BackupApp)

### [Tests](https://github.com/annchous/OopLabs/tree/lab4/OopLabs/BackupAppTest)

### Condition

As part of the laboratory work, it is meant the development of a system that controls the process of creating backups. To simplify the lab, you do not need to physically back up the specified files. It will be enough to create a record that the backup was made.

```
FileRestoreCopyInfo CreateRestore (string filePath)
{
var fileInfo = new FileInfo (filePath);
    var fileRestoreCopyInfo = new FileRestoreCopyInfo (filePath, fileInfo.Size, DateTime.Now);
//File.Copy(filePath, _pathWhereWeNeedToStoreOurBackup); <- This part we can skip
return fileRestoreCopyInfo;
}
```

Backup information is presented as a set of parameters: Id, CreationTime, BackupSize and a list of restore points.

### Creation and storage algorithms

To create a backup, the following objects are specified: a list of files. It should be possible to subsequently edit this list - add and remove objects from the list of objects that will be processed in the algorithm.

The system must support several algorithms for creating restore points for backup, as well as the ability to increase their number. The result of the algorithm is the creation of a new restore point for the specified backup. A recovery point stores information about itself about what objects were backed up in it. The algorithm should be able to indicate whether it is required to create a full-fledged point or only a delta from the last time (i.e. an increment).

It is required to implement at least two storage algorithms:

1. Algorithm of separate storage - files are copied to a special folder and stored there separately.
2. Shared storage algorithm - all objects specified in the backup are added to one archive.

### Point cleaning algorithms

In addition to creation, you need to control the number of stored recovery points. To prevent the accumulation of a large number of old and irrelevant points, it is necessary to implement mechanisms for their cleaning - they must control so that the chain of recovery points does not go beyond the acceptable limit. Within the laboratory, the following types of limits are implied:

1. By quantity - limits the length of the chain of recovery points (we store the last N points)
2. By date - limits how old points will be stored (we store all points that were made no later than the specified date)
3. By size - limits the total size of the backup (we store all the latest backups, the total size of which does not exceed the limit)
4. Hybrid - the ability to combine limits. The user can specify how to combine:
    - you need to delete a point if it went beyond at least one set limit
    - you need to delete the point if it went beyond all the established limits

    For example, the user selects a hybrid of "by quantity" and "by date" algorithms. If according to one of the algorithms it is necessary to leave 3 points, and according to the other - 5, then the number of points is selected in accordance with the parameter specified when creating the "hybrid" (use the maximum or minimum value of the selected points).

The algorithm must take into account that the incremental points should not be left without the point from which the delta is taken. If it was necessary to leave more points than planned, the result of the algorithm should return a corresponding warning.


### Code description
