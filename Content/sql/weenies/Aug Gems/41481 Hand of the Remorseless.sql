DELETE FROM `weenie` WHERE `class_Id` = 41481;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (41481, 'ace41481-handoftheremorseless', 67, '2022-06-02 11:22:52') /* AugmentationDevice */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (41481,   1,        128) /* ItemType - Misc */
     , (41481,   5,         50) /* EncumbranceVal */
     , (41481,  16,          8) /* ItemUseable - Contained */
     , (41481,  19,         100) /* Value */
     , (41481,  33,          1) /* Bonded - Bonded */
     , (41481,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (41481, 114,          1) /* Attuned - Attuned */
     , (41481, 215,         34) /* AugmentationStat */;

INSERT INTO `weenie_properties_int64` (`object_Id`, `type`, `value`)
VALUES (41481,   3, 4000000000) /* AugmentationCost */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (41481,  11, True ) /* IgnoreCollisions */
     , (41481,  13, True ) /* Ethereal */
     , (41481,  14, True ) /* GravityStatus */
     , (41481,  19, True ) /* Attackable */
     , (41481,  22, True ) /* Inscribable */
     , (41481,  69, False) /* IsSellable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (41481,   1, 'Hand of the Remorseless') /* Name */
     , (41481,  16, 'Using this gem increases critical damage by 3%. This augmentation cannot be repeated.') /* LongDesc */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (41481,   1,   33554809) /* Setup */
     , (41481,   3,  536870932) /* SoundTable */
     , (41481,   8,  100686474) /* Icon */
     , (41481,  22,  872415275) /* PhysicsEffectTable */;

/* Lifestoned Changelog:
{
  "LastModified": "2022-06-02T04:21:01.2669114-07:00",
  "ModifiedBy": "pourman",
  "Changelog": [
    {
      "created": "0001-01-01T00:00:00",
      "author": "CrimsonMage",
      "comment": "Congratulations! You have succeeded in acquiring the Hand of the Remorseless augmentation.\r\nViamont Tester has acquired the Hand of the Remorseless augmentation!"
    }
  ],
  "UserChangeSummary": "Congratulations! You have succeeded in acquiring the Hand of the Remorseless augmentation.\r\nViamont Tester has acquired the Hand of the Remorseless augmentation!",
  "IsDone": true
}
*/
